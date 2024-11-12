from llama_parse import LlamaParse
import sys
from groq import Groq


parser = LlamaParse(
    	api_key="llx-***",
    	result_type="text"
    )



def main():
    tekst_iz_pdf = parser.load_data(sys.argv[1])
    i = 0
    pdf_text = ''
    while(i < len(tekst_iz_pdf)):
        pdf_text += tekst_iz_pdf[i].text
        i += 1

    print(pdf_text)



if __name__ == "__main__":
    main()






