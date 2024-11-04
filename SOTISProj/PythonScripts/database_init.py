from neo4j import GraphDatabase
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

def create_nodes_and_relationships(data):
    with driver.session() as session:
        # First pass: creation of nodes
        for item in data:
            session.run(
                """
                MERGE (n:Concept {term: $term})
                SET n.isConcept = true
                """,
                term=item["term"]
            )
        
        # Second pass: connecting terms to their parents
        for item in data:
            if item["superior_term"]:
                session.run(
                    """
                    MATCH (child:Concept {term: $term})
                    MATCH (parent:Concept {term: $superior_term})
                    MERGE (parent)-[:PARENT_OF]->(child)
                    """,
                    term=item["term"],
                    superior_term=item["superior_term"]
                )
                 
def main():
    # BEFORE RUNNING, CHECK THE PATH!!!!!!
    with open("./acm_parse_result.json") as f:
        data = json.load(f)

    create_nodes_and_relationships(data)

    driver.close()
    print("Nodes and relationships created successfully.")

if __name__ == "__main__":
    main()