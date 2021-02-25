window.showToastr = (type, message) => {
  if (type === 'success') {
    // Display a success toast, with a title
    toastr.success(message, 'Operation Successful');
  }
  if (type === 'error') {
    // Display an error toast, with a title
    toastr.error(message, 'Operation Failed');
  }
};
