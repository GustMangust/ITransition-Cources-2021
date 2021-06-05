const { SHA3 } = require('sha3');
const path = require('path');
const fs = require('fs');
const directoryPath = process.cwd();
fs.readdir(directoryPath, function (err, files) {
    const hash = new SHA3(256);
    files.forEach(function (file) {
        if(/(\.[a-z0-9]{2,5})$/i.test(file)){
            fs.readFile(file,"utf8",function(error,data){
                hash.update(data);
                console.log(file.slice(0,file.lastIndexOf("."))+" "+hash.digest('hex'))
            })
        }
    });
});
