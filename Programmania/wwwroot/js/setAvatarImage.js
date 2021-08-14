"use strict";
const setAvatarImage = document.getElementById("uploadAvatarBtn");

setAvatarImage.onclick = openDialogClick;

function openFileDialog(accept, multy = false) {
	var inputElement = document.createElement("input");
	inputElement.type = "file";
	inputElement.accept = accept;
	if(multy) {
		inputElement.multiple = multy;
	}
	

	inputElement.dispatchEvent(new MouseEvent("click")); 
}

function openDialogClick() {
	console.log("True");
	openFileDialog("image/*", false);
}
