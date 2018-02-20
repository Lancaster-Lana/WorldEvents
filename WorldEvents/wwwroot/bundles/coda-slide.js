// when the DOM is ready...
$(document).ready(function () {

	var $panels = $('#slider .scrollContainer > div');
	var $container = $('#slider .scrollContainer');

	// if false, we'll float all the panels left and fix the width 
	// of the container
	var horizontal = false;

	// float the panels left if we're going horizontal
	if (horizontal) {
		$panels.css({
			'float': 'left',
			'position': 'relative' // IE fix to ensure overflow is hidden
		});

		// calculate a new width for the container (so it holds all panels)
		$container.css('width', $panels[0].offsetWidth * $panels.length);
	}

	// collect the scroll object, at the same time apply the hidden overflow
	// to remove the default scrollbars that will appear
	var $scroll = $('#slider .scroll').css('overflow', 'hidden');

	// apply our left + right buttons
	//    $scroll
	//        .before('<img class="scrollButtons left" src="" />')
	//        .after('<img class="scrollButtons right" src="" />');

	// handle nav selection
	function selectNav() {
		$(this)
			.parents('ul:first')
			.find('a')
			.removeClass('selected')
			.end()
			.end()
			.addClass('selected');
	}

	$('#slider .navigation').find('a').click(selectNav);

	// go find the navigation link that has this target and select the nav
	function trigger(data) {
		var el = $('#slider .navigation').find('a[href$="' + data.id + '"]').get(0);
		selectNav.call(el);
	}

	if (window.location.hash) {
		trigger({ id: window.location.hash.substr(1) });
	} else {
		$('ul.navigation a:first').click();
	}

	// offset is used to move to *exactly* the right place, since I'm using
	// padding on my example, I need to subtract the amount of padding to
	// the offset.  Try removing this to get a good idea of the effect
	var offset = parseInt((horizontal ?
		$container.css('paddingTop') :
		$container.css('paddingLeft'))
		|| 0) * -1;


	var scrollOptions = {
		target: $scroll, // the element that has the overflow

		// can be a selector which will be relative to the target
		items: $panels,

		navigation: '.navigation a',

		// selectors are NOT relative to document, i.e. make sure they're unique
		prev: 'img.left',
		next: 'img.right',

		// allow the scroll effect to run both directions
		axis: 'xy',

		onAfter: trigger, // our final callback

		offset: offset,

		// duration of the sliding effect
		duration: 500,

		// easing - can be used with the easing plugin: 
		// http://gsgd.co.uk/sandbox/jquery/easing/
		easing: 'swing'
	};

	// apply serialScroll to the slider - we chose this plugin because it 
	// supports// the indexed next and previous scroll along with hooking 
	// in to our navigation.
	$('#slider').serialScroll(scrollOptions);

	// now apply localScroll to hook any other arbitrary links to trigger 
	// the effect
	$.localScroll(scrollOptions);

	// finally, if the URL has a hash, move the slider in to position, 
	// setting the duration to 1 because I don't want it to scroll in the
	// very first page load.  We don't always need this, but it ensures
	// the positioning is absolutely spot on when the pages loads.
	scrollOptions.duration = 1;
	$.localScroll.hash(scrollOptions);

});
jQuery(function( $ ){
	/**
	 * Most jQuery.localScroll's settings, actually belong to jQuery.ScrollTo, check it's demo for an example of each option.
	 * @see http://flesler.demos.com/jquery/scrollTo/
	 * You can use EVERY single setting of jQuery.ScrollTo, in the settings hash you send to jQuery.LocalScroll.
	 */
	
	// The default axis is 'y', but in this demo, I want to scroll both
	// You can modify any default like this
	$.localScroll.defaults.axis = 'xy';
	
	// Scroll initially if there's a hash (#something) in the url 
	$.localScroll.hash({
		target: '#events_main', // Could be a selector or a jQuery object too.
		queue:true,
		duration:1500
	});
	
	/**
	 * NOTE: I use $.localScroll instead of $('#navigation').localScroll() so I
	 * also affect the >> and << links. I want every link in the page to scroll.
	 */
	$.localScroll({
		target: '#events_main', // could be a selector or a jQuery object too.
		queue:true,
		duration:1500,
		hash:true,
		onBefore:function( e, anchor, $target ){
			// The 'this' is the settings object, can be modified
		},
		onAfter:function( anchor, settings ){
			// The 'this' contains the scrolled element (#content)
		}
	});
});