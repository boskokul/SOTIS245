from groq import Groq
from llama_parse import LlamaParse

def functionLLM(text: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
            f"Extract all key terms and their definitions from the following text, and present the results, in this JSON format:\n"
            "{\n"
            "    'term': 'definition',\n"
            "    'term2': 'definition2'\n"
            "}\n"
            "Additionally, use only those key extracted terms and show all relations between them based on semantic similarity and co-occurrence, in this JSON format:\n"
            "{\n"
            "    'term1': {\n"
            "        'related_to': ['term2', 'term3']\n"
            "    }\n"
            "}\n"
            "Text:\n"
            f"{text}"
        
        }
    ],
    model="llama3-70b-8192",
    )
    return chat_completion.choices[0].message.content



client = Groq(
        api_key="****"
    )	
parser = LlamaParse(
    	api_key="****",
    	result_type="text"
    )


def main():
    tekst_iz_pdf = parser.load_data("..\\SOTISProj\\PDFs\\computers.pdf")
    #print(tekst_iz_pdf)
     
    print(functionLLM(tekst_iz_pdf))

if __name__ == "__main__":
    main()
