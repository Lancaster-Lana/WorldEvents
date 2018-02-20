!function (document, window, $) {
    "use strict";

    $('#external-events .fc-event').each(function () {

        $(this).data('event', {
            title: $.trim($(this).text()),
            stick: true
        });

        $(this).draggable({
            zIndex: 999,
            revert: true,
            revertDuration: 0
        });

    });

    $('#calendar').fullCalendar({
        events: [
            {
                title: 'All Day Event',
                start: '2016-12-20'
            },
            {
                title: 'Hour',
                start: '2016-12-12T17:30:00'
            }],
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        editable: true,
        droppable: true,
		drop: function (date, jsEvent, ui, resourceId) {
			//console.log('drop', date.format(), resourceId);
			debugger;
			var dateFrom = date.format();
			// is the "remove after drop" checkbox checked?
			if ($('#drop-remove').is(':checked')) {
				// if so, remove the element from the "Draggable Events" list
				$(this).remove();
			}
		},
		 // called when a proper external event is dropped
		eventReceive: function (event) {
			debugger;
			console.log('eventReceive', event);
		},

		// called when an event (already on the calendar) is moved
		eventDrop: function (event) {
			debugger;
			console.log('eventDrop', event);
		}
    });

}(document, window, jQuery);
