import os
import sys
import hashlib
if sys.version_info < (3, 6):
    import sha3
for dirs,folder,files in os.walk(os.getcwd()):
    for str in files:
        print(str[0:str.rindex(".")]+" "+hashlib.sha3_256(open(str).read().encode()).hexdigest())
    break