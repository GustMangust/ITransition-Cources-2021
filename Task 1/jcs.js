let wordsArray = process.argv.slice(2), tmpArray="",indexStart=0,serialString="",index = 0,counter = 0;
wordsArray.sort();
if (wordsArray.length === 0){
console.log("");
return;
}
while(index<wordsArray[0].length){
if(counter!=wordsArray.length-1){
indexStart = index;
}
counter=0;
serialString+=wordsArray[0][index];
for(let i = 1;i<wordsArray.length;i++){
if(!wordsArray[i].includes(serialString)){
index = indexStart;
serialString ="";
break;
}
counter++;
}
if(counter==wordsArray.length-1 &&serialString!=""&& serialString.length>=tmpArray.length){
tmpArray = serialString;
}
index++;
}
if(tmpArray==""){
console.log("");
}else{
console.log(tmpArray);
}
