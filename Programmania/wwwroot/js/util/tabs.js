//function openContent(evt, contentName) {
//	// Declare all variables
//	var i, tabcontent, tablinks;

//	// Get all elements with class="tabcontent" and hide them
//	tabcontent = document.getElementsByClassName("tabcontent");
//	for (i = 0; i < tabcontent.length; i++) {
//		tabcontent[i].style.display = "none";
//	}

//	// Get all elements with class="tablinks" and remove the class "active"
//	tablinks = document.getElementsByClassName("tablinks");
//	for (i = 0; i < tablinks.length; i++) {
//		tablinks[i].className = tablinks[i].className.replace(" active", "");
//	}

//	// Show the current tab, and add an "active" class to the button that opened the tab
//	document.getElementById(contentName).style.display = "block";
//	evt.currentTarget.className += " active";
//}


function initTab(button, tab) {
    button.onclick = function () {
        tabs = document.getElementsByClassName('tabcontent');
        for (i = 0; i < tabs.length; i++) {
            tabs[i].style.display = 'none';
        }

        buttons = document.getElementsByClassName('tablinks')
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].classList.remove('active');
        }

        button.classList.add('active');
        tab.style.display = 'block';
    }
}