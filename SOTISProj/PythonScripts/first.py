from groq import Groq
from llama_parse import LlamaParse
import sys
import json
import re

client = Groq(
        api_key="***"
    )

def functionLLM(text: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
            f"Extract all key terms and their definitions from the following text, and present the results, in this valid JSON format:\n"
            "```json{\n"
            "    term: definition,\n"
            "    term2: definition2\n"
            "}```\n"
            "Additionally, use only those key extracted terms and show up to two important relations between each of them based on semantic similarity, in this valid JSON format:\n"
            "```json{\n"
            "    ''term1'': {\n"
            "        ''related_to'': [''term2'', ''term3'']\n"
            "    }\n"
            "}```\n"
            "Text:\n"
            f"{text}"
        
        }
    ],
    model="llama3-70b-8192",
    )
    return chat_completion.choices[0].message.content

	
responseString = functionLLM(sys.argv[1])
# print(responseString)

matches = re.findall(r'```json\n({.*?})\n```', responseString, re.DOTALL)


delimiter = True
for match in matches:
    try:
        data = json.loads(match)
        print(data)
        if delimiter == True:
            print('###')
        delimiter = False
    except json.JSONDecodeError as e:
        print("Error decoding JSON:", e)
        print("No JSON data found.")

# print(relacije[0])


    
    




