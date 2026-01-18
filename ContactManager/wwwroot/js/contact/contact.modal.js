$(() => {
    const $form = $('#contactForm');
    const $modal = $('#contactModal');
    const $modalTitle = $('#modalTitle');
    const $addBtn = $('#addContactBtn');

    const $id = $('input[name="Id"]');
    const $name = $('#Name');
    const $phone = $('#MobilePhone');
    const $job = $('#JobTitle');
    const $birth = $('#BirthDate');

    let lastFocusedElement = null;

    const clearFormState = () => {
        $form[0]?.reset();

        const validator = $form.data('validator');
        validator?.resetForm();

        $form.find('.is-valid, .is-invalid')
            .removeClass('is-valid is-invalid');

        $form.find('[aria-invalid]')
            .attr('aria-invalid', 'false');

        $form.find('[data-valmsg-for]')
            .text('');

        $form.find(':input').each((_, el) => {
            el.setCustomValidity?.('');
        });
    };

    $(document).on('click', '[data-bs-target="#contactModal"]', e => {
        lastFocusedElement = e.currentTarget;
    });

    $addBtn.on('click', () => {
        clearFormState();
        $modalTitle.text('Create contact');
        $form.attr('action', '/Contact/Create');
    });

    $(document).on('click', '.edit-btn', function () {
        clearFormState();

        $modalTitle.text('Edit contact');
        $form.attr('action', '/Contact/Edit');

        $id.val($(this).data('id'));
        $name.val($(this).data('name'));
        $phone.val($(this).data('phone'));
        $job.val($(this).data('job'));
        $birth.val($(this).data('birth'));
    });

    $modal.on('hide.bs.modal', () => {
        clearFormState();
        $(lastFocusedElement || $addBtn).trigger('focus');
    });

    $modal.on('hidden.bs.modal', () => {
        lastFocusedElement = null;
    });
});
