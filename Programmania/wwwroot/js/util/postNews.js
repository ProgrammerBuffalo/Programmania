const elements = document.querySelectorAll('.newsBlock__settings__btn');

elements.forEach(element => {
	element.addEventListener('click',() => {
		let command = element.dataset['element'];

		if(command == 'createLink' || command == 'insertImage'){
			let url = prompt("Enter link here", "https://www.google.com");
			document.execCommand(command,false,url);
		}

		document.execCommand(command, false, null)
	});
})