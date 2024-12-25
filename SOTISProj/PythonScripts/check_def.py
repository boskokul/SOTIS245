from neo4j import GraphDatabase
import sys
from groq import Groq

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

def get_definition_for_term(term):
    with driver.session() as session:
        result = session.run("""
            MATCH (node)
            WHERE node.term = $term AND node.isConcept = false
            RETURN node.term AS term, node.definition AS definition
        """, term=term)
        
        record = result.single()
        if record:
            return record["definition"]
        else:
            return None
        
client = Groq(
        api_key="***"
    )	

def functionLLM(term: str, definition1: str, definition2: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
            f"If these two defintions of the term are similar then just say correct if not just say incorrect:\n"
            "Term:\n"
            f"{term}"
            "First definition:\n"
            f"{definition1}"
            "Second definition:\n"
            f"{definition2}"
        }
    ],
    model="llama3-70b-8192",
    )
    return chat_completion.choices[0].message.content

	

if __name__ == "__main__":
    # term_to_search = "Stateful"
    term_to_search = sys.argv[1]
    # definition_answ = "some network protocol that transmits a single message"
    definition_answ = sys.argv[2]
    definition = get_definition_for_term(term_to_search)
    if definition:
        # print(definition)
        responseString = functionLLM(term_to_search, definition, definition_answ)
        print(responseString)
    else:
        print(f"Definition for term '{term_to_search}' not found.")
    
    driver.close()
