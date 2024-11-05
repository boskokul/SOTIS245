from neo4j import GraphDatabase
import json

URI = "bolt://localhost:7687"
AUTH = ("neo4j", "testtest")

driver = GraphDatabase.driver(URI, auth=AUTH)


# Define the query
query = """
MATCH (parent)-[:PARENT_OF]->(child)
WHERE parent.term = $term
RETURN child
"""

# Execute the query
def get_children(term):
    with driver.session() as session:
        result = session.run(query, term=term)
        # Extract only properties and convert them to a list of dictionaries
        children = [record["child"]._properties for record in result]
    return children


        
                 
def main():
    
    # Get child nodes of the specified parent term
    children = get_children("General and reference")

    # Process or print the retrieved data
    for child in children:
        print(child)

    # Close the connection
    driver.close()
    print("Nodes and relationships created successfully.")

if __name__ == "__main__":
    main()