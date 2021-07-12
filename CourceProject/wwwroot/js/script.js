$(document).ready(function () {
    let a = document.getElementsByClassName("fanfic-description");
    for (let i = 0; i < a.length; i++) {
        if (a.item(i).innerHTML.length >= 140) {
            if (a.item(i).innerHTML.charAt(140) != ' ') {
                var newString = a.item(i).innerHTML.substring(0, 140);
                var words = newString.split(' ');
                a.item(i).innerHTML = '';
                for (let j = 0; j < words.length - 1; j++) {
                    a.item(i).innerHTML += words[j] + ' ';
                }
                a.item(i).innerHTML += '...';
            } else {
                a.item(i).innerHTML = a.item(i).innerHTML.substring(0, 140) + '...';
            }
        }
    }

});
