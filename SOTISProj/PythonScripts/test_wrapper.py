import json
import sys
from test_connect_terms import get_connect_question
from test_def_terms import get_def_terms

#retrieving system arguments
rootTerm_sys = sys.argv[1]
conn_num_json = sys.argv[2]
def_num_json = sys.argv[3]

conn_num_json = conn_num_json.replace("{", '{"').replace(":", '":').replace(",", ',"')
conn_num_json = conn_num_json.replace('"{', '{')
# print(conn_num_json)
#defining parameters for functions
connect_questions_nums = [(item["termNum"], item["parentNum"]) for item in json.loads(conn_num_json)]
def_questions_num = int(def_num_json)
root_term = "Networks" #NOT IMPLEMENTED!!!!!!!!!!!!!!!! IN OTHER SCRIPTS root_term is fixed -- to be fixed soon

connect_questions = []
for i in range(len(connect_questions_nums)):
    conncect_question = get_connect_question(connect_questions_nums[i][0], connect_questions_nums[i][1])
    #print(conncect_question)
    connect_questions.append(conncect_question)

def_questions_json = get_def_terms(def_questions_num)

parsed_connect_questions = [
    json.loads(item) if isinstance(item, str) else item for item in connect_questions
]
connect_questions_data = {
    "connect_questions" : parsed_connect_questions
}

connect_questions_json = json.dumps(connect_questions_data, indent=4)
merged_json_string = connect_questions_json[:-1] + "," + def_questions_json[1:]
print(merged_json_string)



