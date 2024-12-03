import json
from neo4j import GraphDatabase
import sys
import re

# Connect to the Neo4j database
uri = "bolt://localhost:7687"  # Replace with your Neo4j URI
username = "neo4j"  # Replace with your Neo4j username
password = "testtest"  # Replace with your Neo4j password
driver = GraphDatabase.driver(uri, auth=(username, password))

# Define the query to retrieve paths with parent and children side-by-side
query = """
MATCH path = (root {term: $term})-[:PARENT_OF|RELATED_TO*]->(descendant)
WITH [node IN nodes(path) | {term: node.term, isConcept: node.isConcept}] AS hierarchy
RETURN hierarchy
"""

# Execute the query and parse results into a list of paths
def get_paths(term):
    with driver.session() as session:
        result = session.run(query, term=term)
        paths = [record["hierarchy"] for record in result]
    return paths


# Convert the list of paths into a nested tree structure
def build_tree(paths):
    tree = {"properties": {"term": paths[0][0]["term"]}, "children": []}

    # Helper function to recursively add nodes to the tree
    def add_to_tree(current_node, path):
        for node in path:
            # Check if the node already exists as a child
            child = next((item for item in current_node["children"] if item["properties"]["term"] == node["term"]), None)
            if child is None:
                # If not, add the node as a new child
                child = {"properties": node, "children": []}
                current_node["children"].append(child)
            # Move to the next level in the tree
            current_node = child

    # Build the tree by adding each path
    for path in paths:
        # Skip the root node itself in each path
        add_to_tree(tree, path[1:])
    
    return tree

# Retrieve paths and convert them to a tree structure
paths = get_paths(sys.argv[1])

hierarchy_tree = build_tree(paths)

# Convert to JSON and print
json_output = json.dumps(hierarchy_tree, indent=2)
print(json_output)

pattern = r'"term":\s*"([^"]+)"'

# Find all matches
matches = re.findall(pattern, json_output)

# print('###')
# termsAll = set()
# # Print all term names
# for match in matches:
#     termsAll.add(match)
# print(termsAll)

# Close the connection
driver.close()
