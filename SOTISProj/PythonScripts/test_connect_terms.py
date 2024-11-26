from neo4j import GraphDatabase
import sys
import random
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)

root_term = "Networks" #will change


#function for getting terms that need to be connected
def get_terms_connection():
    with driver.session() as session:
        result = session.run("""MATCH (root)-[:PARENT_OF*]->(parent)-[:RELATED_TO]->(node)
                                WHERE root.term = $term AND node.isConcept = false
                                WITH node
                                ORDER BY rand()
                                LIMIT 5
                                RETURN node
                                """, term = root_term)
        terms = [record["node"]._properties for record in result]
        return terms
    
#function for finding the acm node of the term that needs to be connected
def get_term_parent(term):
    with driver.session() as session:
        result = session.run(""" MATCH (parent)-[:RELATED_TO]->(node)
                                WHERE node.term = $term AND parent.isConcept = true
                                LIMIT 1
                                RETURN parent
                                """, term = term)
        parent = [record["parent"]._properties for record in result]
        return parent
    
#function to get random terms so that connection is not one on one
def get_random_terms(excludedTerms):
    with driver.session() as session:
        result = session.run("""MATCH (root)-[:PARENT_OF*]->(parent)
                                WHERE root.term = $term 
                                AND (NOT parent.term IN $excludedTerms)
                                WITH parent
                                ORDER BY rand()
                                LIMIT 3
                                RETURN  parent
                                """, term = root_term,excludedTerms= excludedTerms)
        terms = [record["parent"]._properties["term"] for record in result]
        return terms
#function for creation of list of temrs and shuffled list of parents
def create_terms_lists():
    terms_list = []
    parent_terms = []
    terms = get_terms_connection()
    for term in terms:
        terms_list.append(term["term"])
        parent = get_term_parent(term["term"])
        parent_terms.append(parent[0]["term"])
        #print(str(term["term"]) + " ; " +  parent[0]["term"])

    random_terms = get_random_terms(parent_terms)
    
    for random_term in random_terms:
        parent_terms.append(random_term) 
    print(parent_terms)
    random.shuffle(parent_terms) #shuffling to change the order
    return terms_list, parent_terms
    
def convert_to_json(terms, parents):
    data_terms = {
    "terms": terms
    }

    data_parents = {
    "parents": parents
}
    json_terms = json.dumps(data_terms, indent=4)
    json_parents = json.dumps(data_parents, indent=4)
    return json_terms, json_parents

def main():
    terms, parents = create_terms_lists()
    terms_jason, parents_json = convert_to_json(terms, parents)
    print(terms_jason)
    print("-----")
    print(parents_json)
    driver.close()

main()