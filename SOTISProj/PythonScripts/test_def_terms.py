from neo4j import GraphDatabase
import sys
import random
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

root_term = "Networks"
def get_terms_definitions():
    with driver.session() as session:
        result = session.run("""MATCH (root)-[:PARENT_OF*]->(parent)-[:RELATED_TO]->(node)
                                WHERE root.term = $term AND node.isConcept = false
                                WITH node
                                ORDER BY rand()
                                LIMIT 5
                                RETURN node
                                """, term = root_term)
        terms = [{"term": record["node"]._properties["term"], "definition": record["node"]._properties["definition"]} for record in result]
        return terms
    
def convert_to_json():
    terms_json = json.dumps(get_terms_definitions(), indent=4)
    print(terms_json)
    
def main():
    # terms = get_terms_definitions()
    # for term in terms:
    #     print(term)
    convert_to_json()
    driver.close()

main()


