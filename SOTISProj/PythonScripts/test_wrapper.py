import json
import sys
from test_connect_terms import get_connect_question
from test_def_terms import get_def_terms

connect_questions_nums = [(3,5),(4,4),(5,6)] #to become dynamic
def_questions_num = 5 #to become dynamic

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



