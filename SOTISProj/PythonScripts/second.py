from groq import Groq
from llama_parse import LlamaParse
import sys
import json
import re

client = Groq(
        api_key="***"
    )
	
def functionLLM(terms: str, concepts: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
            f"Find relationships between these terms and concepts based on their semantic similarity, please ignore if they are the same ones (plural or singular form, etc). For each term find up to one relationship with one concept that is most similar. Present the results, in this valid JSON format:\n"
            "```json{\n"
            "    term: concept,\n"
            "    term2: concept2\n"
            "}```\n"
            "\nTerms:\n"
            f"{terms}"
            "\nConcepts:\n"
            f"{concepts}"
        
        }
    ],
    model="llama-3.3-70b-versatile",
    )
    return chat_completion.choices[0].message.content

	
responseString = functionLLM(sys.argv[1], sys.argv[2])
# print(sys.argv[1])
print(responseString)

import re
pattern = r'"([^"]+)":\s*"([^"]+)"'
matches = re.findall(pattern, responseString)
# term_concept_dict = {term: concept for term, concept in matches}
# print("Extracted Term-Concept Pairs:") 
# print(matches)




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

    create_nodes_and_relationships(matches)

    driver.close()
    print("Nodes and relationships created successfully.")


    
create()




