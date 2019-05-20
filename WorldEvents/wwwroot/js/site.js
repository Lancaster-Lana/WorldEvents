
$(function () {
	//Remove cached data in modal window
	$('body').on('hidden.bs.modal', '.modal', function () {
		$(this).removeData('bs.modal');
	});

	$('body').on('shown.bs.modal', function () {
		var $form = $('form'); //modal - dialog

		$form.submit(function (e) {
			e.preventDefault();
			/*var form = $('.modal-dialog form');
			form.validate({
				rules: {
					name: "required",
					email: {
						required: true,
						email: true
					},
					phone: {
						required: true,
						phone: true
					}
				},
				messages: {
					name: "Please let us know who you are.",
					email: "A valid email will help us get in touch with you.",
					phone: "Please provide a valid phone number."
				}
			});
			if (!form.valid()) {
				return;
			}
			else {
				 $.ajax({
					 url: this.action,
					 type: this.method,
					 data: $(this).serialize(),
					 success: function (result) {
 
						 //if (result.success) {
							 $(form).modal('hide');
							 location.reload(); //Refresh
						 //} else {
							 //$('#myModalContent').html(result);
							 //bindForm();
						 //}
					 }
				 });
				 return false;
			 };*/
		});
		//     
		//        var model = $(modal).serializeObject();
		//        $.ajax({
		//            type: 'post',
		//            dataType: 'json',
		//            contentType: app.consts.contentTypes.formUrlencoded,
		//            url: form.action,
		//            data: model
		//        }).done(function (response) {
		//            abp.message.success('Patient agreement is signed successfully', ' Success!');
		//            window.location = response.targetUrl;
		//        }).fail(function (response) {
		//            var msgError = response.error;
		//            abp.message.error(msgError);
		//        }).always(function (response) {
		//            abp.ui.clearBusy('.page-content');
		//        });

		//
	});
});