from neo4j import GraphDatabase
import sys

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)


# Function to check the correctness of a single term pair
def check_single_term_pair(term, parent):
    with driver.session() as session:
        result = session.run("""MATCH (parent)-[:RELATED_TO]->(node)
                                WHERE node.term = $term AND parent.term = $parent
                                RETURN node
                                """, term=term, parent=parent)
        if result.single() is not None:
            return True
        else:
            return False


# Example usage
if __name__ == "__main__":
    term = sys.argv[1]
    parent = sys.argv[2]

    is_correct = check_single_term_pair(term, parent)

    if is_correct:
        print("correct")
    else:
        print("incorrect")

    driver.close()
