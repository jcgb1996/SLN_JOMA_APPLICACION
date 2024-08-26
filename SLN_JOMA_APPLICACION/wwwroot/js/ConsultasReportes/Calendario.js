var Calendario = {
    Init: function () {
        /*=================*/
        //  Calender Date variable
        /*=================*/
        var newDate = new Date();
        function getDynamicMonth() {
            var getMonthValue = newDate.getMonth();
            var _getUpdatedMonthValue = getMonthValue + 1;
            return _getUpdatedMonthValue < 10 ? `0${_getUpdatedMonthValue}` : `${_getUpdatedMonthValue}`;
        }

        /*=================*/
        // Calender Modal Elements
        /*=================*/
        var $modal = $('#eventModal');
        var $getModalTitleEl = $("#event-title");
        var $getModalStartDateEl = $("#event-start-date");
        var $getModalEndDateEl = $("#event-end-date");
        var $getModalAddBtnEl = $(".btn-add-event");
        var $getModalUpdateBtnEl = $(".btn-update-event");
        var calendarsEvents = {
            Danger: "danger",
            Success: "success",
            Primary: "primary",
            Warning: "warning",
        };

        /*=====================*/
        // Calendar Elements and options
        /*=====================*/
        var $calendarEl = $("#calendar");
        var checkWidowWidth = function () {
            return $(window).width() <= 1199;
        };
        var calendarHeaderToolbar = {
            left: "prev next addEventButton",
            center: "title",
            right: "dayGridMonth,timeGridWeek,timeGridDay",
        };
        var calendarEventsList = [
            {
                id: 1,
                title: "Event Conf.",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-01`,
                extendedProps: { calendar: "Danger" },
            },
            {
                id: 2,
                title: "Seminar #4",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-07`,
                end: `${newDate.getFullYear()}-${getDynamicMonth()}-10`,
                extendedProps: { calendar: "Success" },
            },
            {
                groupId: "999",
                id: 3,
                title: "Meeting #5",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-09T16:00:00`,
                extendedProps: { calendar: "Primary" },
            },
            {
                groupId: "999",
                id: 4,
                title: "Submission #1",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-16T16:00:00`,
                extendedProps: { calendar: "Warning" },
            },
            {
                id: 5,
                title: "Seminar #6",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-11`,
                end: `${newDate.getFullYear()}-${getDynamicMonth()}-13`,
                extendedProps: { calendar: "Danger" },
            },
            {
                id: 6,
                title: "Meeting 3",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-12T10:30:00`,
                end: `${newDate.getFullYear()}-${getDynamicMonth()}-12T12:30:00`,
                extendedProps: { calendar: "Success" },
            },
            {
                id: 7,
                title: "Meetup #",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-12T12:00:00`,
                extendedProps: { calendar: "Primary" },
            },
            {
                id: 8,
                title: "Submission",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-12T14:30:00`,
                extendedProps: { calendar: "Warning" },
            },
            {
                id: 9,
                title: "Attend event",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-13T07:00:00`,
                extendedProps: { calendar: "Success" },
            },
            {
                id: 10,
                title: "Project submission #2",
                start: `${newDate.getFullYear()}-${getDynamicMonth()}-28`,
                extendedProps: { calendar: "Primary" },
            },
        ];

        /*=====================*/
        // Calendar Select fn.
        /*=====================*/
        var calendarSelect = function (info) {
            $getModalAddBtnEl.show();
            $getModalUpdateBtnEl.hide();
            $modal.modal('show');
            $getModalStartDateEl.val(info.startStr);
            $getModalEndDateEl.val(info.endStr);
        };

        /*=====================*/
        // Calendar AddEvent fn.
        /*=====================*/
        var calendarAddEvent = function () {
            var currentDate = new Date();
            var dd = String(currentDate.getDate()).padStart(2, "0");
            var mm = String(currentDate.getMonth() + 1).padStart(2, "0");
            var yyyy = currentDate.getFullYear();
            var combineDate = `${yyyy}-${mm}-${dd}T00:00:00`;
            $getModalAddBtnEl.show();
            $getModalUpdateBtnEl.hide();
            $modal.modal('show');
            $getModalStartDateEl.val(combineDate);
        };

        /*=====================*/
        // Calender Event Function
        /*=====================*/
        var calendarEventClick = function (info) {
            var eventObj = info.event;

            if (eventObj.url) {
                window.open(eventObj.url);
                info.jsEvent.preventDefault();
            } else {
                var getModalEventId = eventObj._def.publicId;
                var getModalEventLevel = eventObj._def.extendedProps[ "calendar" ];
                var $getModalCheckedRadioBtnEl = $(`input[value="${getModalEventLevel}"]`);

                $getModalTitleEl.val(eventObj.title);
                $getModalStartDateEl.val(eventObj.startStr.slice(0, 10));
                $getModalEndDateEl.val(eventObj.endStr.slice(0, 10));
                $getModalCheckedRadioBtnEl.prop('checked', true);
                $getModalUpdateBtnEl.attr("data-fc-event-public-id", getModalEventId);
                $getModalAddBtnEl.hide();
                $getModalUpdateBtnEl.show();
                $modal.modal('show');
            }
        };

        /*=====================*/
        // Active Calender
        /*=====================*/
        var calendar = new FullCalendar.Calendar($calendarEl[ 0 ], {
            selectable: true,
            height: checkWidowWidth() ? 900 : 1052,
            initialView: checkWidowWidth() ? "listWeek" : "dayGridMonth",
            initialDate: `${newDate.getFullYear()}-${getDynamicMonth()}-07`,
            headerToolbar: calendarHeaderToolbar,
            events: calendarEventsList,
            select: calendarSelect,
            unselect: function () {
                console.log("unselected");
            },
            customButtons: {
                addEventButton: {
                    text: "Add Event",
                    click: calendarAddEvent,
                },
            },
            eventClassNames: function ({ event: calendarEvent }) {
                const getColorValue = calendarsEvents[ calendarEvent._def.extendedProps.calendar ];
                return [ "event-fc-color", "fc-bg-" + getColorValue ];
            },
            eventClick: calendarEventClick,
            windowResize: function (arg) {
                if (checkWidowWidth()) {
                    calendar.changeView("listWeek");
                    calendar.setOption("height", 900);
                } else {
                    calendar.changeView("dayGridMonth");
                    calendar.setOption("height", 1052);
                }
            },
        });

        /*=====================*/
        // Update Calender Event
        /*=====================*/
        $getModalUpdateBtnEl.on("click", function () {
            var getPublicID = $(this).data("fc-event-public-id");
            var getTitleUpdatedValue = $getModalTitleEl.val();
            var setModalStartDateValue = $getModalStartDateEl.val();
            var setModalEndDateValue = $getModalEndDateEl.val();
            var getEvent = calendar.getEventById(getPublicID);
            var $getModalUpdatedCheckedRadioBtnEl = $('input[name="event-level"]:checked');

            var getModalUpdatedCheckedRadioBtnValue =
                $getModalUpdatedCheckedRadioBtnEl.length > 0
                    ? $getModalUpdatedCheckedRadioBtnEl.val()
                    : "";

            getEvent.setProp("title", getTitleUpdatedValue);
            getEvent.setDates(setModalStartDateValue, setModalEndDateValue);
            getEvent.setExtendedProp("calendar", getModalUpdatedCheckedRadioBtnValue);
            $modal.modal('hide');
        });

        /*=====================*/
        // Add Calender Event
        /*=====================*/
        $getModalAddBtnEl.on("click", function () {
            var $getModalCheckedRadioBtnEl = $('input[name="event-level"]:checked');

            var getTitleValue = $getModalTitleEl.val();
            var setModalStartDateValue = $getModalStartDateEl.val();
            var setModalEndDateValue = $getModalEndDateEl.val();
            var getModalCheckedRadioBtnValue =
                $getModalCheckedRadioBtnEl.length > 0 ? $getModalCheckedRadioBtnEl.val() : "";

            calendar.addEvent({
                id: 12,
                title: getTitleValue,
                start: setModalStartDateValue,
                end: setModalEndDateValue,
                allDay: true,
                extendedProps: { calendar: getModalCheckedRadioBtnValue },
            });
            $modal.modal('hide');
        });

        /*=====================*/
        // Calendar Init
        /*=====================*/
        calendar.render();
        var myModal = new bootstrap.Modal(document.getElementById("eventModal"));
        var modalToggle = $(".fc-addEventButton-button");
        $("#eventModal").on("hidden.bs.modal", function (event) {
            $getModalTitleEl.val("");
            $getModalStartDateEl.val("");
            $getModalEndDateEl.val("");
            var $getModalIfCheckedRadioBtnEl = $('input[name="event-level"]:checked');
            if ($getModalIfCheckedRadioBtnEl.length > 0) {
                $getModalIfCheckedRadioBtnEl.prop('checked', false);
            }
        });

    }
}