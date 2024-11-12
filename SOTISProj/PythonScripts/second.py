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
            f"Find relationships between these terms and concepts based on their semantic similarity. For each term find zero or one relationship with one concept that is most similar!"
            "\nTerms:\n"
            f"{terms}"
            "\nConcepts:\n"
            f"{concepts}"
        
        }
    ],
    model="llama3-70b-8192",
    )
    return chat_completion.choices[0].message.content

	
responseString = functionLLM(sys.argv[1], sys.argv[2])
# print(sys.argv[1])
print(responseString)


    
    




