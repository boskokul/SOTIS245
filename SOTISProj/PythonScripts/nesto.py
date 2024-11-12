import openai

openai.api_base = "https://openrouter.ai/api/v1"
openai.api_key = "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"



fe = """Here are some terms and relations between them, I want to bind it to the tree structure that I have, can you somehow find what to bind together in these two structures?
First:
{
    "Computer": {
        "related_to": ["Hardware", "Software", "Operating System", "Peripherals"]
    },
    "Hardware": {
        "related_to": ["Computer", "Peripherals"]
    },
    "Software": {
        "related_to": ["Computer", "Operating System"]
    },
    "Operating System": {
        "related_to": ["Computer", "Software"]
    },
    "Peripherals": {
        "related_to": ["Computer", "Hardware"]
    },
    "Input": {
        "related_to": ["Process", "Output", "Data"]
    },
    "Process": {
        "related_to": ["Output", "Input", "Data"]
    },
    "Output": {
        "related_to": ["Input", "Process", "Data"]
    },
    "Data": {
        "related_to": ["Input", "Process", "Output"]
    },
    "Advantages": {
        "related_to": ["High Speed"]
    },
    "High Speed": {
        "related_to": ["Advantages"]
    },
    "Generation": {
        "related_to": ["Fifth Generation", "Fourth Generation"]
    },
    "Fifth Generation": {
        "related_to": ["Generation", "ULSI technology"]
    },
    "Fourth Generation": {
        "related_to": ["Generation", "VLSI technology"]
    },
    "ULSI technology": {
        "related_to": ["Fifth Generation", "VLSI technology"]
    },
    "VLSI technology": {
        "related_to": ["Fourth Generation", "ULSI technology"]
    },
    "Parallel Processing": {
        "related_to": ["Fifth Generation", "Artificial Intelligence"]
    },
    "Artificial Intelligence": {
        "related_to": ["Parallel Processing", "Fifth Generation"]
    },
    "Natural Language Processing": {
        "related_to": ["Fifth Generation", "Artificial Intelligence"]
    },
    "Superconductor technology": {
        "related_to": ["Fifth Generation", "Advancement"]
    },
    "Advancement": {
        "related_to": ["Superconductor technology", "Fifth Generation"]
    },
    "User-friendly interfaces": {
        "related_to": ["Fifth Generation", "Multimedia features"]
    },
    "Multimedia features": {
        "related_to": ["User-friendly interfaces", "Fifth Generation"]
    },
    "Desktop": {
        "related_to": ["Laptop", "Notebook", "Ultrabook", "Chromebook"]
    },
    "Laptop": {
        "related_to": ["Desktop", "Notebook", "Ultrabook", "Chromebook"]
    },
    "Notebook": {
        "related_to": ["Desktop", "Laptop", "Ultrabook", "Chromebook"]
    },
    "Ultrabook": {
        "related_to": ["Desktop", "Laptop", "Notebook", "Chromebook"]
    },
    "Chromebook": {
        "related_to": ["Desktop", "Laptop", "Notebook", "Ultrabook"]
    }
}
Second:
{
  "properties": {
    "term": "Software and its engineering"
  },
  "children": [
    {
      "properties": {
        "term": "Software organization and properties",
        "isConcept": true
      },
      "children": [
        {
          "properties": {
            "term": "Contextual software domains",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "E-commerce infrastructure",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software infrastructure",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Interpreters",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Middleware",
                    "isConcept": true
                  },
                  "children": [
                    {
                      "properties": {
                        "term": "Message oriented middleware",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Reflective middleware",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Embedded middleware",
                        "isConcept": true
                      },
                      "children": []
                    }
                  ]
                },
                {
                  "properties": {
                    "term": "Virtual machines",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Operating systems",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "File systems management",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Memory management",
                    "isConcept": true
                  },
                  "children": [
                    {
                      "properties": {
                        "term": "Virtual memory",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Main memory",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Allocation / deallocation strategies",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Garbage collection",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Distributed memory",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Secondary storage",
                        "isConcept": true
                      },
                      "children": []
                    }
                  ]
                },
                {
                  "properties": {
                    "term": "Process management",
                    "isConcept": true
                  },
                  "children": [
                    {
                      "properties": {
                        "term": "Scheduling",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Deadlocks",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Multithreading",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Multiprocessing / multiprogramming / multitasking",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Monitors",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Mutual exclusion",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Concurrency control",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Power management",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Process synchronization",
                        "isConcept": true
                      },
                      "children": []
                    }
                  ]
                },
                {
                  "properties": {
                    "term": "Communications management",
                    "isConcept": true
                  },
                  "children": [
                    {
                      "properties": {
                        "term": "Buffering",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Input / output",
                        "isConcept": true
                      },
                      "children": []
                    },
                    {
                      "properties": {
                        "term": "Message passing",
                        "isConcept": true
                      },
                      "children": []
                    }
                  ]
                }
              ]
            },
            {
              "properties": {
                "term": "Virtual worlds software",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Interactive games",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Virtual worlds training simulations",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            }
          ]
        },
        {
          "properties": {
            "term": "Software system structures",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Embedded software",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software architectures",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "n-tier architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Peer-to-peer architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Data flow architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Cooperating communicating processes",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Layered systems",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Publish-subscribe / event-based architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Electronic blackboards",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Simulator / interpreter",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Object oriented architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Tightly coupled architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Space-based architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "3-tier architectures",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Software system models",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Petri nets",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "State systems",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Entity relationship modeling",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Model-driven software engineering",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Feature interaction",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Massively parallel systems",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Ultra-large-scale systems",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Distributed systems organizing principles",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Cloud computing",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Client-server architectures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Grid computing",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Organizing principles for web applications",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Real-time systems software",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Abstraction, modeling and modularity",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Software functional properties",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Correctness",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Synchronization",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Functionality",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Real-time schedulability",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Consistency",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Completeness",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Access protection",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Formal methods",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Model checking",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Software verification",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Automated static analysis",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Dynamic analysis",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            }
          ]
        },
        {
          "properties": {
            "term": "Extra-functional properties",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Interoperability",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software performance",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software reliability",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software fault tolerance",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Checkpoint / restart",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Software safety",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software usability",
                "isConcept": true
              },
              "children": []
            }
          ]
        }
      ]
    },
    {
      "properties": {
        "term": "Software notations and tools",
        "isConcept": true
      },
      "children": [
        {
          "properties": {
            "term": "General programming languages",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Language types",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Parallel programming languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Distributed programming languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Imperative languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Object oriented languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Functional languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Concurrent programming languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Constraint and logic languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Data flow languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Extensible languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Assembly languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Multiparadigm languages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Very high level languages",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Language features",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Abstract data types",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Polymorphism",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Inheritance",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Control structures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Data types and structures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Classes and objects",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Modules / packages",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Constraints",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Recursion",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Concurrent programming structures",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Procedures, functions and subroutines",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Patterns",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Coroutines",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Frameworks",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            }
          ]
        },
        {
          "properties": {
            "term": "Formal language definitions",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Syntax",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Semantics",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Compilers",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Interpreters",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Incremental compilers",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Retargetable compilers",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Just-in-time compilers",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Dynamic compilers",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Translator writing systems and compiler generators",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Source code generation",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Runtime environments",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Preprocessors",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Parsers",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Context specific languages",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Markup languages",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Extensible Markup Language (XML)",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Hypertext languages",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Scripting languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Domain specific languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Specialized application languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "API languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Graphical user interface languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Window managers",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Command and control languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Macro languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Programming by example",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "State based definitions",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Visual languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Interface definition languages",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "System description languages",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Design languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Unified Modeling Language (UML)",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Architecture description languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "System modeling languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Orchestration languages",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Integration frameworks",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Specification languages",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Development frameworks and environments",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Object oriented frameworks",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software as a service orchestration system",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Integrated and visual development environments",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Application specific development environments",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Software configuration management and version control systems",
            "isConcept": true
          },
          "children": []
        },
        {
          "properties": {
            "term": "Software libraries and repositories",
            "isConcept": true
          },
          "children": []
        },
        {
          "properties": {
            "term": "Software maintenance tools",
            "isConcept": true
          },
          "children": []
        }
      ]
    },
    {
      "properties": {
        "term": "Software creation and management",
        "isConcept": true
      },
      "children": [
        {
          "properties": {
            "term": "Designing software",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Requirements analysis",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software design engineering",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software design tradeoffs",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software implementation planning",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Software design techniques",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            }
          ]
        },
        {
          "properties": {
            "term": "Software development process management",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Software development methods",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Rapid application development",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Agile software development",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Capability Maturity Model",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Waterfall model",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Spiral model",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "V-model",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Design patterns",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Risk management",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Software development techniques",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Software prototyping",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Object oriented development",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Flowcharts",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Reusability",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Software product lines",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Error handling and recovery",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Automatic programming",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Genetic programming",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            }
          ]
        },
        {
          "properties": {
            "term": "Software verification and validation",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Software prototyping",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Operational analysis",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software defect analysis",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Software testing and debugging",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Fault tree analysis",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Process validation",
                "isConcept": true
              },
              "children": [
                {
                  "properties": {
                    "term": "Walkthroughs",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Pair programming",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Use cases",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Acceptance testing",
                    "isConcept": true
                  },
                  "children": []
                },
                {
                  "properties": {
                    "term": "Traceability",
                    "isConcept": true
                  },
                  "children": []
                }
              ]
            },
            {
              "properties": {
                "term": "Formal software verification",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Empirical software validation",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Software post-development issues",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Software reverse engineering",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Documentation",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Backup procedures",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software evolution",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Software version control",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Maintaining software",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "System administration",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Collaboration in software development",
            "isConcept": true
          },
          "children": [
            {
              "properties": {
                "term": "Open source model",
                "isConcept": true
              },
              "children": []
            },
            {
              "properties": {
                "term": "Programming teams",
                "isConcept": true
              },
              "children": []
            }
          ]
        },
        {
          "properties": {
            "term": "Search-based software engineering",
            "isConcept": true
          },
          "children": []
        }
      ]
    }
  ]
}"""


response = openai.ChatCompletion.create(
  model="meta-llama/llama-3.2-3b-instruct:free",
  messages=[
       { 'role': "user", 'content': {fe} }
  ],
  headers={
  },
)

reply = response.choices[0].message
print(reply.content)