import os
import re

def fix_relative_references():
    path_to_node_modules = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'node_modules')

    for root, dirs, files in os.walk(path_to_node_modules):
        for file in files:
            if file.endswith('.js'):
                file_path = os.path.join(root, file)
                with open(file_path, 'r') as f:
                    file_content = f.read()
                    #file_content = re.sub(r"from '([^/\.~])", r"from '/\1", file_content)
                    #file_content = re.sub(r"from \"([^/\.~])", r"from \"/\1", file_content)
                    #file_content = re.sub(r"import '([^/\.~])", r"import '/\1", file_content)
                    #file_content = re.sub(r"import \"([^/\.~])", r"import \"/\1", file_content)
                    #file_content = re.sub(r"require\('([^/\.~])", r"require('/\1", file_content)
                    #file_content = re.sub(r"require\(\"([^/\.~])", r"require(\"/\1", file_content)
                    #file_content = re.sub("@polymer", "/node_modules/@polymer", file_content)
                    file_content = re.sub("@webcomponents", "/node_modules/@webcomponents", file_content)
                    #print(re.search("polymer-legacy", file_content))
                    if (re.search("@polymer/polymer/polymer-legacy.js", file_content) != None):
                        print(file_path)
                with open(file_path, 'w') as f:
                    f.write(file_content)

fix_relative_references()
