<?php
    foreach (glob("*.*") as $filename) {
        echo "$filename"." ".hash('sha3-256' , file_get_contents($filename))."\n";
    }
?>