from groq import Groq
from llama_parse import LlamaParse

def funkcijaLLM(tekst_njegov: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
           f"What do you think about? "
        f"{tekst_njegov}"
        }
    ],
    model="llama3-70b-8192",
    )
    return chat_completion.choices[0].message.content

def presloviLLM(tekst_njegov: str):
    chat_completion = client.chat.completions.create(
    messages=[
        {
            "role": "user",
            "content": 
           f"Pretvori sledeci tekst sa srpskog jezika napisanog u cirilici u latinicno pismo na sprskom jeziku!\n"
        "Tekst:\n"
        f"{tekst_njegov}"
        
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

#tekst_iz_pdf = parser.load_data("./genetika.pdf")
#print(tekst_iz_pdf)
#tekst_latinica = presloviLLM(tekst_iz_pdf)
#print(tekst_latinica)
#tekst_konacan = funkcijaLLM(tekst_latinica)
#print(tekst_konacan)

def main():
    print("Hello from Python!")
     
    print(funkcijaLLM("Nikola Jokic"))

if __name__ == "__main__":
    main()
