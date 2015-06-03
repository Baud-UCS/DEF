$(document).ready(function () {
	$('.screenshots').bxSlider({
		captions: true
	});
	
	$('.image-link').magnificPopup({
		type: 'image',
		zoom: {
			enabled: true, // By default it's false, so don't forget to enable it
			duration: 200
		}
	});
});