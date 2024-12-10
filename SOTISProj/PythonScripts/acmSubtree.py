import json
from neo4j import GraphDatabase
from concurrent.futures import ThreadPoolExecutor, as_completed
import re

# Connect to the Neo4j database
uri = "bolt://localhost:7687"
username = "neo4j"
password = "testtest"
driver = GraphDatabase.driver(uri, auth=(username, password))

# Define the query to retrieve paths with parent and children side-by-side
parent_query = """
MATCH path = (root {term: $term})-[:PARENT_OF*]->(descendant)
WITH [node IN nodes(path) | {term: node.term, isConcept: node.isConcept}] AS hierarchy
RETURN hierarchy
"""

# Query to retrieve RELATED_TO relationships
related_query = """
MATCH (a)-[:RELATED_TO]->(b)
WHERE a.term IN $terms OR b.term IN $terms
RETURN a.term AS source, b.term AS target, b.isConcept AS isConcept
"""

# Query to retrieve all RELATED_TO relationships
related_query_all = """
MATCH (a)-[:RELATED_TO]->(b)
RETURN a.term AS source, b.term AS target, b.isConcept AS isConcept
"""

# Execute the query and parse results into a list of paths
def get_paths(term):
    with driver.session() as session:
        result = session.run(parent_query, term=term)
        paths = [record["hierarchy"] for record in result]
    return paths

# Execute the query to retrieve RELATED_TO relationships
def get_related_to(terms):
    with driver.session() as session:
        result = session.run(related_query, terms=terms)
        related_to = [(record["source"], {"term": record["target"], "isConcept": record["isConcept"]}) for record in result]
    return related_to

def get_related_all(terms):
    with driver.session() as session:
        result = session.run(related_query_all, terms=terms)
        related_to = [(record["source"], {"term": record["target"], "isConcept": record["isConcept"]}) for record in result]
    return related_to

# Convert the list of paths into a nested tree structure using dictionaries
def build_tree(paths):
    tree = {"properties": {"term": paths[0][0]["term"]}, "children": []}
    nodes = {paths[0][0]["term"]: tree}

    # Helper function to recursively add nodes to the tree
    def add_to_tree(current_node, path):
        for node in path:
            term = node["term"]
            if term not in nodes:
                new_child = {"properties": node, "children": []}
                current_node["children"].append(new_child)
                nodes[term] = new_child
            current_node = nodes[term]

    # Build the tree by adding each path
    for path in paths:
        add_to_tree(tree, path[1:])

    return tree, nodes

# Use ThreadPoolExecutor to parallelize path retrieval
def get_all_paths(term):
    paths = []
    with ThreadPoolExecutor(max_workers=4) as executor:
        futures = [executor.submit(get_paths, term)]
        for future in as_completed(futures):
            paths.extend(future.result())
    return paths

# Retrieve paths and convert them to a tree structure
paths = get_all_paths("Networks")

hierarchy_tree, nodes = build_tree(paths)

# Get the terms in the tree to fetch RELATED_TO relationships
terms = list(nodes.keys())
related_to_edges = get_related_to(terms)

# Add RELATED_TO connections to the tree and their children
for source, target in related_to_edges:
    if source not in nodes:
        nodes[source] = {"properties": {"term": source}, "children": []}
    source_node = nodes[source]
    
    if target["term"] not in nodes:
        new_child = {"properties": target, "children": []}
        nodes[target["term"]] = new_child
        source_node["children"].append(new_child)
    else:
        target_node = nodes[target["term"]]
        if "related_to" not in source_node:
            source_node["related_to"] = []
        source_node["related_to"].append(target_node)
        # Adding related-to terms as children
        if "children" not in source_node:
            source_node["children"] = []
        source_node["children"].append(target_node)
        

related_to_all = get_related_all(terms)
# print(related_to_all)
done = []
for source, target in related_to_all:
    source_node = nodes[source]
    if source_node["properties"]["isConcept"] == False:
        continue
    target_node = nodes[target["term"]]
    if(source_node["properties"]["term"] in done):
        continue
    done.append(target["term"])
    for sourceNew, targetNew in related_to_all:
        target_node_new = nodes[targetNew["term"]].copy()
        if sourceNew == target["term"]:  
            # print(target_node["properties"]["term"], target_node_new["properties"]["term"])
            source_node_new = nodes[sourceNew]
            new_child = {"properties": targetNew, "children": []}
            # continue
            if "children" not in target_node:
                target_node["children"] = []
            target_node["children"].append(new_child)


# Convert to JSON and print
json_output = json.dumps(hierarchy_tree, indent=2)
print(json_output)

pattern = r'"term":\s*"([^"]+)"'

# Find all matches
matches = re.findall(pattern, json_output)

# Print all term names
# terms_all = set(matches)
# print(terms_all)

# Close the connection
driver.close()
