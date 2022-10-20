let index = 0;

const swalDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-dark btn-sm btn-block btn-outline-dark'
    },
    buttonsStyling: false
})

function AddTag() {
    var tagEntry = document.getElementById("tagEntry");

    let searchResult = Search(tagEntry.value);
    if (searchResult != null) {
        swalDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult}</span>`,
            icon: 'error'
        });
    }
    else {
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("tagList").options[index++] = newOption;
    }

    tagEntry.value = "";
    return true;
}

function RemoveTag() {
    let tagCount = 1;

    let tagList = document.getElementById("tagList");

    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalDarkButton.fire({
            html: `<span class='font-weight-bolder'>Chose a tag before deleting</span>`,
            icon: 'error'
        });
        return true;
    }

    while (tagCount > 0) { 
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else 
            tagCount = 0;
        index--;     
    }
}

$("form").on("submit", function () {
    $("#tagList option").prop("selected", "selected");
})

if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("tagList").options[index] = newOption;
}

function Search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsList = document.getElementById('tagList');
    if (tagsList) {
        let options = tagsList.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `Tag #${str} already exists`;
            }
        }
    }
}

