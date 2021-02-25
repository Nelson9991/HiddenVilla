window.showToastr = (type, message) => {
	if (type === 'success') {
		// Display a success toast, with a title
		toastr.success(message, 'Operation Successful');
	}
	if (type === 'error') {
		// Display an error toast, with a title
		toastr.error(message, 'Operation Failed');
	}
}

window.showSwal = (type, message, headingTitle) => {
	if (type === 'success') {
		Swal.fire({
			title: headingTitle,
			text: message,
			icon: 'success'
		});
	}
	if (type === 'error') {
		Swal.fire({
			title: headingTitle,
			text: message,
			icon: 'error'
		});
	}
}

function handleDeleteConfirmationModal(isOpen) {

	if (isOpen) {
		const modal = new bootstrap.Modal(document.getElementById('deleteConfirmationModal'));
		modal.show();
	} else {
		const myModalEl = document.getElementById('deleteConfirmationModal');
		const modal = bootstrap.Modal.getInstance(myModalEl);
		modal.hide();
	}

}

