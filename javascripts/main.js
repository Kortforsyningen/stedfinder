console.log('This would be the main JS file.');

function load() {
    var file = new XMLHttpRequest();
    file.open("GET", "https://raw.githubusercontent.com/geodatastyrelsen/stedfinder/master/changelog.md", true);
    file.onreadystatechange = function() {
      if (file.readyState === 4) {  // Makes sure the document is ready to parse
        if (file.status === 200) {  // Makes sure it's found the file
          text = file.responseText;
          document.getElementById("change-log").innerHTML = text;
        }
      }
    }
}

window.onLoad = load();