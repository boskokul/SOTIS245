from bs4 import BeautifulSoup
import json

def html_parse(file_path):
    with open(file_path, "r", encoding="utf-8") as file:
        html_content = file.read()

    soup = BeautifulSoup(html_content, "html.parser")
    parse_result = []
    hierarchy_stack = []

    def parse_ul(ul_element):
        for li in ul_element.find_all("li", recursive=False):
            a_tag = li.find("a", attrs={"data-uri": True})
            if a_tag:
                term = a_tag.get_text(strip=True)
                parent_data = hierarchy_stack[-1] if hierarchy_stack else None
                parse_result.append({"term": term, "superior_term": parent_data})

                nested_ul = li.find("ul")
                if nested_ul:
                    hierarchy_stack.append(term)  
                    parse_ul(nested_ul)  
                    hierarchy_stack.pop() 

    first_ul = soup.find("ul")
    if first_ul:
        parse_ul(first_ul)

    return parse_result


file_path = "./ACMHTMLWHOLECHUNK.html"
parse_result = html_parse(file_path)


output_file = "./acm_parse_result.json"
with open(output_file, "w", encoding="utf-8") as json_file:
    json.dump(parse_result, json_file, indent=2)

#print("Parse and write finished")
