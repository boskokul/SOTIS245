import sys

for_adding = []
pairs = sys.argv[2].split(';')
for p in pairs[:-1]:
    # print(p)
    pair = p.split(':')
    # print(pair[0])
    # print('aaaaaaaa')
    # print(pair[1])
    for_adding.append([pair[0], pair[1]])

for_creating = []
terms_defs = sys.argv[3].split(';')
for p in terms_defs[:-1]:
    # print(p)
    pair = p.split(':')
    # print(pair[0])
    # print('sssss')
    # print(pair[1])
    for_creating.append([pair[0], pair[1]])


from neo4j import GraphDatabase
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)
# driver = None

def create_nodes_and_relationships(data_for_creation, data):
    with driver.session() as session:
        # First pass: creation of nodes
        for termx, definitiony in data_for_creation:
            session.run(
                """
                MERGE (n:Concept {term: $term })
                SET n.isConcept = false, n.definition = $definition
                """,
                term=termx,
                definition=definitiony
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

    create_nodes_and_relationships(for_creating, for_adding)

    driver.close()
    print("Nodes and relationships created successfully.")


    
create()
