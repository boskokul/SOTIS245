from openai import OpenAI
from os import getenv
import sys
import json

client = OpenAI(
  base_url="https://openrouter.ai/api/v1",
  api_key=""
)

completion = client.chat.completions.create(
  model="meta-llama/llama-3.1-70b-instruct:free",
  messages=[
    {
      "role": "user",
      "content": 
      f"Extract key terms and show relations between them based on semantic similarity in the following text while using only terms that you extracted and return results in the JSON format containing terms, definitions and relationships. And nothing else beside that please:\n"
                "Text:\n"
                f"{sys.argv[1]}"
    }
  ]
)

responseString = completion.choices[0].message.content
print(responseString)

# # Parse the JSON string
# data = json.loads(responseString)

# # Collect all unique terms from keys and related terms
# terms = set(data.keys())  # Start with the main terms (keys)
# # for key, value in data.items():
# #     terms.update(value["related_to"])  # Add related terms from "related_to"

# # # Convert the set to a sorted list
# # sorted_terms = sorted(terms)

# # # Print the sorted list of terms
# print(terms)



