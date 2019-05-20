!function (document, window, $) {
	"use strict";

	//Draw external events
	$('#external-events .fc-event').each(function () {

		$(this).data('event',
			{
				title: $.trim($(this).text()),
				stick: true
			});

		$(this).draggable({
			zIndex: 999,
			revert: true,
			revertDuration: 0
		});
	});

	//Init calendar scheduler
	$('#calendarCtrl').fullCalendar({

		schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives', //only for development
		header: {
			left: 'today,prev,next',
			center: 'title',
			right: 'agendaDay,agendaWeek,month'
		},
		editable: true,
		droppable: true,
		//events: [
		//    { id: '1', resourceId: 'b', start: '2017-09-07T02:00:00', end: '2017-09-07T07:00:00', title: 'event 1' },
		//    { id: '2', resourceId: 'c', start: '2017-09-07T05:00:00', end: '2017-09-07T22:00:00', title: 'event 2' },

        /*googleCalendarApiKey: '<YOUR API KEY>',
        events: {
            googleCalendarId: 'lan.levinovska@group.calendar.google.com',
            className: 'gcal-event' // an option!
        },*/       //]

		events: function (start, end, timezone, callback)
		{
			$.ajax({
				url: '/Event/GetCalendarData',
				type: "GET",
				dataType: "JSON",

				success: function (result) {
					var events = [];

					$.each(result, function (i, data) {
						events.push(
							{
								id: data.Id,
								title: data.Title,
								allDay: false,//data.allDay,
								description: data.Desc,
								start: data.StartDate, //moment(data.StartDate).format('YYYY-MM-DD'),
								end: data.EndDate, //moment(data.EndDate).format('YYYY-MM-DD'),
								backgroundColor: "#9501fc",
								borderColor: "#fc0101"
							});
					});

					callback(events);
				},

				error: function (jqXHR, textStatus, errorThrown) {
					alert(errorThrown);
				}
			});
		},

		select: function (start, end, jsEvent, view, resource) {
			console.log(
				'select day !!!',
				start.format(),
				end.format(),
				resource ? resource.id : '(no resource)'
			);
		},

		drop: function (date, jsEvent, ui, resourceId) {
			//console.log('drop', date.format(), resourceId);
			var dateFrom = date.format();

			// is the "remove after drop" checkbox checked?
			if ($('#drop-remove').is(':checked')) {
				// if so, remove the element from the "Draggable Events" list
				$(this).remove();
			}
		},

		// called when an event (already on the calendar): moved to ather day, etc
		eventDrop: function (event)
		{
			console.log('eventDrop', event);
			//TOOD: refresh event StartDate in DB : ask dialog to save to db
			//EditEventDialog(event);
			//$('#calendarCtrl').fullCalendar('updateEvent', event);
		},

		//A new external event is dropped to calendar
		eventReceive: function (event) {
			//Show 'Create event' dialog
			CreateEventDialog(event);
		},

		eventClick: function (event)
		{
			EditEventDialog(event);
		},

		dayClick: function (date, jsEvent, view, resource) {
			//console.log('dayClick', date.format(), resource ? resource.id : '(no resource)');

			$(this).on('mousedown', function (e) {
				//if (e.which == 3)
				{
					$.contextMenu({//show context menu on right click
						selector: '.fc-day', //'.day-view',
						callback: function (key, options) {
							switch (key) {
								case "addEvent":
									CreateEventDialog();
									break;
								case "delete":
									//DeleteEventDialog(jsEvent);
									break;
							}
						},
						items: {
							"addEvent": { name: "Create", icon: "create", url: "/Event/Create" }
							//"clearEvents": { name: "Quit", icon: function ($element, key, item) { return 'context-menu-icon context-menu-icon-quit'; } }
						}
					});
				}
			});
		},

		//Draw event and attach handlers 
		eventRender: function (event, element)
		{
			element.attr('data-event-id', event.id);
			element.addClass('context-class');

			element.on('mousedown', function (e) {
				//show context menu on right click
				if (e.which == 3) {
					//$(this).find('a.fc-event').contextMenu({
					$.contextMenu({
						selector: '.fc-event',
						callback: function (key, options) {
							//var m = "clicked: " + key;
							//window.console && console.log(m) || alert(m);
							switch (key) {
								case "addEvent":
									CreateEventDialog();
									break;
								case "delete":
									DeleteEventDialog(event);
									break;
							}
						},
						items: {
							"addEvent": { name: "Create", icon: "create", url: "/Event/Create" },
							"editEvents": { name: "Edit", icon: "edit", url: "/Event/Edit" },
							"copy": { name: "Copy", icon: "copy" },
							"paste": { name: "Paste", icon: "paste" },
							"delete": { name: "Delete", icon: "delete", url: "/Event/Delete" },
							"quit": { name: "Quit", icon: function ($element, key, item) { return 'context-menu-icon context-menu-icon-quit'; } }
						}
					});
				}
			});
            /*element.qtip(
                {
                    content: event.description
                });*/
		},

		// hide div after fullcalendar has initialized
		eventAfterAllRender: function (view) {

		}

	});

	function CreateEventDialog(event)
	{
		//var calCtrl = $('#calendarCtrl');
		//var element = $(calCtrl);
		//var $calendar = element.data('fullCalendar');

		var url = "/Event/Create";

		var eventName = event != null ? event.title : "NewEvent";
		var startDate = (event != null ? event.start : new Date()).toISOString(); //.new Date( start.toLocaleDateString("en-US", options))/ event.start.toJSON(); //Date.parse
		var endDate = startDate;

		//INIT content for 'Create event' dialog
		$("#eventContent").load(url,
			{
				Title: eventName,
				EventType: eventName,
				StartDate: startDate,
				EndDate: endDate
			},
			function (data) {
				//Show 'Create event' dialog
				$("#eventModal").dialog(
					{
						title: "Create a new " + eventName,
						modal: true,
						draggable: true,
						width: 'auto',
						open: function () {
							//init participants list (with checkboxes): multiselect.js - JQuery lib
							//$('#participantDD').multiselect({ includeSelectAllOption: true });
						},
						close: function () {
							//clear content
							$("#eventContent").empty();
							//$(this).close();
						},
						buttons:
						{
							Cancel: function () {
								//remove 'canceled for creation' event 'from Calender'
								$('#calendarCtrl').fullCalendar('removeEvents', function (searchEvent) {
									return searchEvent._id === event._id;
								});

								$("#eventModal").dialog('close');
							},
							Save: function () {
								var updatedTitle = $("#EventTitle").val();
								var updatedType = $("#EventType").val();

								var dateNow = moment().format('YYYY-MM-DD');

								var newStartDate = $("#StartDate").val();
								var newStartTime = $("#StartTime").val(); //$.fullCalendar.formatDate(date, 'HH:mm')
								var newEndDate = $("#EndDate").val();
								var newEndTime = $("#EndTime").val();

								if (dateNow > newStartDate) {
									alert("Start date must be in future !");
								}
								else if (newEndDate >= newStartDate) {
									//Check time
									if (newEndDate == newStartDate) {
										//end time must be greater then start
										if (newEndTime <= newStartTime) {
											alert("Wrong end time !");
											return;
										}
									}

									event.allDay = false; //TODO:
									event.title = updatedTitle;
									event.eventType = updatedType;
									event.start = newStartDate + ' ' + newStartTime;
									event.end = newEndDate + ' ' + newEndTime;//moment(newEndDate + ' ' + newEndTime); //newEndDate + 'T' + newEndTime + " A";//"Z";
									//get selected participants
									/*var selectedUsers = new Array();
									$('select#participantDD option:selected').each(function () {
										selectedUsers.push(this.value);
									});*/

									var participantsListStr = $('select#participantDD').val();

									var model = //JSON.stringify(
									{
										Title: event.title,//Name: event.title,
										EventType: event.eventType,
										StartDate: event.start,
										EndDate: event.end
										//Participants = participantsListStr
									};
									//Add event to DB (via eventService\eventManager)
									//$.post("/Event/Register", model, function(response) 
									//{ 
									//  $(this).dialog('close');
									//});							

									$.ajax({
										type: "POST",
										url: "/Event/Register",
										data: model,
										error: function (response) {
											alert(response.responseText);
										},
										success: function (response) {
											//After event created in DB update the event in calendar control
											$('#calendarCtrl').fullCalendar('updateEvent', event);

											$("#eventModal").dialog('close');//Close modal
										}
									});
								}
								else {
									alert("Wrong end date !");
								}
							}
						}
					});

				//init handler for button "New participant" in event dialog
				$('#newParticipant').on('click', function () {
					//show popup with email and fio
					swal('Fill participant data');
				});
			});
	}

	function DeleteEventDialog(event)
	{
		var id = event._id;
		var deleteEventUrl = '/Event/Delete/' + id;

		swal({
			title: "Delete event",
			text: "Are you sure to delete event '" + event.title + "' ?",
			icon: "warning",
			buttons: [
				'No',
				'Yes, Delete it!'
			],
			dangerMode: true,
		}).then(function (isConfirm) {
			if (isConfirm)
			{
				//Remove from calendar
				$('#calendarCtrl').fullCalendar('removeEvents', function (searchEvent) {
					return searchEvent._id === event._id;
				});

				//Remove from DB
				$.ajax({
					url: deleteEventUrl,
					success: function () {
						swal({
							title: 'Success!',
							text: 'Event has been deleted!',
							icon: 'success'
						}).then(function ()
						{
							//form.submit(); // <--- submit form programmatically
						});
					}
				});
			} else {
				swal("Cancelled", "Event is saved", "error");
			}
		})
	}

	function EditEventDialog(event)
	{
		var url = '/Event/Details';

		var start = moment(event.start).format('YYYY-MM-DD HH:mm A');
		var end = moment(event.end).format('YYYY-MM-DD HH:mm A');
		if (!event.eventType)
			event.eventType = event.title;

		var model = //JSON.stringify(
		{
			AllDay: false,
			Id: event.id,
			EventType: event.eventType,
			Title: event.title,
			StartDate: start,
			EndDate: end
		};

		// Edit event dialog
		$("#eventContent").load(url, model,
			function (res) {
				//show details dialog
				$("#eventModal").dialog(
					{
						title: event.title,
						modal: true,
						width: 'auto',
						close: function () {
							//clear content
							$("#eventContent").empty();
							//$(this).close();
						},
						buttons:
						{
							Cancel: function () {
								$(this).dialog('close');
							},
							Save: function () {

								var updatedTitle = $("#EventTitle").val();
								var eventType = $("#EventType").val();
								var dateNow = moment().format('YYYY-MM-DD');
								var newStartDate = $("#StartDate").val(); var newStartTime = $("#StartTime").val();
								var newEndDate = $("#EndDate").val(); var newEndTime = $("#EndTime").val();

								//Add event to DB (via eventService\eventManager)
								if (dateNow > newStartDate) {
									alert("Start date must be in future !");
								}
								else if (newEndDate >= newStartDate) {
									//Check time
									if (newEndDate == newStartDate) {
										//end time must be greater then start
										if (newEndTime <= newStartTime) {
											alert("Wrong end time !");
											return;
										}
									}

									event.title = updatedTitle;
									event.eventType = eventType;
									event.allDay = false; //TODO:
									event.start = moment(newStartDate + ' ' + newStartTime); //format("HH:mm:ss")
									event.end = moment(newEndDate + ' ' + newEndTime); //newEndDate + 'T' + newEndTime + " A";//"Z";

									//Create\update the event in calendar if dates range is valid
									$('#calendarCtrl').fullCalendar('updateEvent', event);

									//Close modal
									$(this).dialog('close');
								}
								else {
									alert("Check end date, please !");
								}
							}
						}
					});

				//init handler for button "New participant" in event dialog
				$('#newParticipant').on('click', function () {
					//show popup with email and fio
					alert('Fill participant data');

				});
			}
		);
	}

}(document, window, jQuery);
