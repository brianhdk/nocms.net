;(function(window, document, undefined) {
	"use strict";
	
	document.addEventListener("DOMContentLoaded", function () {

		var modal = document.createElement('div');
		modal.id = "nocms-modal-container";
		
		modal.addEventListener("keypress", function(e) {
			console.log(e.which, e.target);
		});
		
		document.querySelector('body').appendChild(modal);

		var elements = document.querySelectorAll('[data-nocms]');

		[].forEach.call(elements, function(element) {

			element.addEventListener("click", function () {

				modal.className = "active";
				
				var context = JSON.parse(element.getAttribute('data-nocms'));

				var iframe = '<div><iframe seamless src="/nocms/assets/?file=app/editor/default.html"></iframe></div>';

				modal.innerHTML = iframe;
			});
		});
	});

})(window, document);