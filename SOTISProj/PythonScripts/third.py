import sys

for_adding = []
pairs = sys.argv[2].split(';')
for p in pairs[:-1]:
    # print(p)
    pair = p.split(':')
    # print(pair[1])
    for_adding.append([pair[0], pair[1]])


from neo4j import GraphDatabase
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

def create_nodes_and_relationships(data):
    with driver.session() as session:
        # First pass: creation of nodes
        for termx, concepty in data:
            session.run(
                """
                MERGE (n:Concept {term: $term})
                SET n.isConcept = false
                """,
                term=termx
            )
        
        # Second pass: connecting terms to their concepts
        for termx, concepty in data:
            session.run(
                """
                MATCH (child:Concept {term: $term})
                MATCH (parent:Concept {term: $superior_term})
                MERGE (parent)-[:RELATED_TO]->(child)
                """,
                term=termx,
                superior_term=concepty
                )
                 
def create():

    create_nodes_and_relationships(for_adding)

    driver.close()
    print("Nodes and relationships created successfully.")


    
create()
