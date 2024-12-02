from neo4j import GraphDatabase
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

root_term = "Networks"
def get_terms_definitions(question_num):
    with driver.session() as session:
        result = session.run("""MATCH (root)-[:PARENT_OF*]->(parent)-[:RELATED_TO]->(node)
                                WHERE root.term = $term AND node.isConcept = false
                                WITH node
                                ORDER BY rand()
                                LIMIT $question_num
                                RETURN node
                                """, term = root_term, question_num = question_num)
        terms = [record["node"]._properties["term"] for record in result]
        return terms
    
def get_def_terms(question_num):
    data_terms = {
    "def_terms": get_terms_definitions(question_num)
    }
    terms_json = json.dumps(data_terms, indent=4)
    return terms_json
    


#main()


