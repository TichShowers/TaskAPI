//function Task(id, text, status, user) {
//    this.id = id;
//    this.text = text;
//    this.status = status;
//    this.user = user;
//};

//function User(id, name) {
//    this.id = id;
//    this.name = name;
//};

var urlStart = "http://localhost:52907/api/";

var saveTasks = function (tasks) {
    localStorage.tasks = JSON.stringify(tasks);
};

var saveUsers = function (users) {
    localStorage.users = JSON.stringify(users);
};

var seedTasks = function () {
    return [
    new Task(1, "Opstaan", "F", 1),
    new Task(2, "Patatten kopen", "F", 2),
    new Task(3, "Lesgeven", "F", 3),
    new Task(4, "Niet inschrijven", "F", 4),
    new Task(5, "Naar school gaan", "F", 1),
    new Task(6, "Patatten schillen", "F", 2),
    new Task(7, "Hearthstone spelen", "U", 6),
    new Task(8, "Examens geven", "U", 3),
    new Task(9, "Alles", "U", 4),
    new Task(10, "Guideway afwerken", "U", 5),
    new Task(11, "1,000 sigaretten roken", "F", 5),
    new Task(12, "Lesvolgen", "F", 1),
    new Task(13, "Naar huis gaan", "U", 1),
    new Task(14, "Slapen", "U", 1),
    new Task(15, "Patatten snijden", "F", 2),
    new Task(16, "Afstuderen", "U", 4),
    new Task(17, "Frieten bakken", "U", 2),
    new Task(18, "Frieten eten", "U", 2),
    new Task(19, "Rotzooi opruimen", "U", 2)

    ];
};

var nextTaskId = function () {
    if (!localStorage.tasks && !localStorage.taskIdentity) {
        localStorage.taskIdentity = 19;
    }
    localStorage.taskIdentity = parseInt(localStorage.taskIdentity) + 1;
    return parseInt(localStorage.taskIdentity);

};

var seedUsers = function () {
    return [
    new User(1, "Colin"),
    new User(2, "Koen"),
    new User(3, "Joris"),
    new User(4, "Kris"),
    new User(5, "Stijn"),
    new User(6, "Michiel")
    ];
};

var loadTasks = function () {
    $.ajax(urlStart + "task", { DataType: "json", type:"GET", error: function(jqxhr, textStatus, errorThrown) {}, success: tasksLoadedSuccess });
};

var loadUsers = function () {
    $.ajax(urlStart + "user", { DataType: "json", type: "GET", error: function (jqxhr, textStatus, errorThrown) { }, success: usersLoadedSuccess });
};

var loadLoggedInUser = function () {
    $.ajax(urlStart + "user/default", { DataType: "json", type: "GET", error: function (jqxhr, textStatus, errorThrown) { }, success: curUserLoadedSuccess });
};

var saveNewUser = function(user) {
    $.ajax(urlStart + "user",
        {
            DataType: "json",
            type: "POST",
            error: function (jqxhr, textStatus, errorThrown) { },
            success: saveNewUserSuccess,
            data: user
        });
}

var saveNewTask = function (task) {
    $.ajax(urlStart + "task",
        {
            DataType: "json",
            type: "POST",
            error: function (jqxhr, textStatus, errorThrown) { },
            success: saveNewTaskSuccess,
            data: task
        });
}

var saveTask = function (task) {
    $.ajax(urlStart + "task/" + task.Id,
        {
            DataType: "json",
            type: "PUT",
            error: function (jqxhr, textStatus, errorThrown) { },
            success: saveTaskSuccess,
            data: task
        });
}

var saveUser = function (user) {
    $.ajax(urlStart + "user/" + user.Id,
        {
            DataType: "json",
            type: "PUT",
            error: function (jqxhr, textStatus, errorThrown) { },
            success: saveTaskSuccess,
            data: user
        });
}

var deleteTask = function(task) {
    $.ajax(urlStart + "task/" + task.Id,
    {
        DataType: "json",
        type: "DELETE",
        error: function(jqxhr, textStatus, errorThrown) {},
        success: deleteTaskSuccess
    });
};

var updateUserLocation = function (user, location) {
    $.ajax(urlStart + "user/location/" + user.Id,
        {
            DataType: "json",
            type: "PUT",
            error: function (jqxhr, textStatus, errorThrown) { },
            success: updateUserLocationSuccess,
            data: location
        });
}

var saveLoggedInUser = function (user) {
    localStorage.currentUsers = JSON.stringify(user);
};

var users;
var tasks;
var curUser;

var initialize = function () {
    $('#members-list').hide();
    $('#edit-who').selectmenu();
    $('#new-who').selectmenu();
    $('#edit-status').slider();

    loadLoggedInUser();
    loadUsers();
    loadTasks();

    $('#edit-submit').bind('click', editTodo);
    $('#new-submit').bind('click', saveNewTodo);
    $('#delete-task').bind('click', deleteTodo);
    $('#user-edit').bind('click', function() {
        var username = $('#username').val();
        saveUser({ Name: username });
    });

    $('#user-new').bind('click', function () {
        curUser.Name = $('#username').val();
        saveNewUser(curUser);
    });

    $('#main-list ul').on('click', '.task-button', taskButtonHandler);
    $('#members-list ul').on('click', '.user-button', userButtonHandler);

    //saveUsers(users);
};

var printUsername = function () {
    $('#user').text(curUser.Name);
}

var taskButtonHandler = function (evt) {
    var id = $(this).data("id");

    var curTask = $.grep(tasks, function (task) {
        return id === task.Id;
    })[0];

    $('#edit-id').attr('value', curTask.Id);
    $('#edit-text').attr('value', curTask.Description);
    $('#edit-status').val(curTask.Status);
    $('#edit-who').val(curTask.UserFor);
    $('#edit-who').selectmenu('refresh');
    $('#edit-status').slider('refresh');
};

var userButtonHandler = function (evt) {
    var id = $(this).data("id");

    curUser = $.grep(users, function (user) {
        return id === user.Id;
    })[0];

    saveLoggedInUser(curUser);
    printUsername();
    refreshAllTasks();
    $('#task-tab').trigger('click');
};

var saveNewTodo = function (e) {
    e.preventDefault();
    var taskText = $('#new-text').val();
    var taskWho = parseInt($('#new-who').val());

    saveNewTask({ "Description": taskText, "Status": "U", "UserFor": taskWho });
};

var editTodo = function (e) {
    e.preventDefault();
    var taskId = parseInt($('#edit-id').val());
    var taskText = $('#edit-text').val();
    var taskWho = parseInt($('#edit-who').val());
    var taskStatus = $('#edit-status').val();

    saveTask({ "Id": taskId, "Description": taskText, "Status": taskStatus, "UserFor": taskWho });
};

var deleteTodo = function (e) {
    e.preventDefault();
    var taskId = parseInt($('#edit-id').val());
    deleteTask($.grep(tasks, function(t) { return t.Id === taskId; })[0]);
};

var refreshAllTasks = function () {
    $.each(tasks, function (key, task) {
        $('#task_' + task.Id).remove();
    });

    printAllTasks(tasks);
}

var printAllTasks = function(tasks) {
    $.each(tasks.sort(function(a, b) { return sortFunction(b, a, function(task) { return task.text; }); }), function(key, task) {
        printTask(task);
    });

    tasks.sort(function(a, b) { return sortFunction(a, b, function(task) { return task.id; }); });
};

var printAllUsers = function(users) {
    $.each(users, function(key, user) {
        printUser(user);
    });
}

var refreshUserDropdowns = function(users) {
    $('.dropdown').empty();
    $.each(users, function(key, value) {
        var option = $('<option>').attr('value', value.Id).text(value.Name);

        $('.dropdown-who').append(option);
    });

    $('#edit-who').selectmenu('refresh');
    $('#new-who').selectmenu('refresh');

    navigator.geolocation.getCurrentPosition(function (position) {
        updateUserLocation(curUser, { "Latitude": position.coords.latitude, "Longitude": position.coords.longitude });
    });

};

var printTask = function (task) {
    var item = $('<li>')
        .attr('id', 'task_' + task.Id)
        .attr('class', 'ui-alt-icon ui-nodisc-icon')
        .append($('<a>')
        .attr('href', "#edit")
        .attr("data-id", task.Id)
        .attr("data-role", "button")
        .attr("class", "ui-btn task-button ui-btn-icon-right ui-icon-carat-r")
        .text(task.Description));

    if (task.Status === "F") {
        $("#lst-done").after(item);
    } else if (task.UserFor === curUser.Id) {
        $("#lst-mine").after(item);
    } else {
        $("#lst-other").after(item);
    }
    $('#main-list ul').listview('refresh');

    refreshListviewTotals(tasks);
};

var printUser = function (user) {
    var item = $('<li>')
        .attr('id', 'task_' + user.Id)
        .attr('class', 'ui-alt-icon ui-nodisc-icon')
        .append($('<a>')
        .attr('href', "#")
        .attr("data-id", user.Id)
        .attr("data-role", "button")
        .attr("class", "ui-btn user-button ui-btn-icon-right ui-icon-home")
        .text(user.Name));

    $('#members-list ul').append(item);
    $('#members-list ul').listview('refresh');
}

var refreshListviewTotals = function (tasks) {
    var finished = $.grep(tasks, function (task) { return task.Status === "F"; }).length;
    var mine = $.grep(tasks, function (task) { return task.Status !== "F" && task.UserFor === curUser.Id; }).length;
    var other = $.grep(tasks, function (task) { return task.Status !== "F" && task.UserFor !== curUser.Id; }).length;

    $('#lst-done span').text(finished);
    $('#lst-mine span').text(mine);
    $('#lst-other span').text(other);
};

var tasksLoadedSuccess = function (data, textStatus, jqxhr) {
    console.log(data);
    tasks = data;
    printAllTasks(data);
}

var usersLoadedSuccess = function(data, textStatus, jqxhr) {
    users = data;
    printAllUsers(users);
    refreshUserDropdowns(users);
}

var curUserLoadedSuccess = function(data, textStatus, jqxhr) {
    curUser = data;
    printUsername();
    if(users)
        refreshUserDropdowns(users);
}

var saveNewUserSuccess = function(data, textStatus, jqxhr) {
    users.push(data);
    curUser = data;
    printUsername();
    printUser(data);
    refreshUserDropdowns(users);
};

var saveUserSuccess = function(data, textStatus, jqxhr) {
    curUser = data;
    users = $.grep(users, function(user) { return user.Id != data.Id; });
    users.push(data);
    printUsername();
    refreshUserDropdowns(users);
    $.mobile.changePage("#main");
};

var saveTaskSuccess = function (data, textStatus, jqxhr) {
    console.log(data);

    var task = $.grep(tasks, function (t) { return t.Id === data.Id; })[0];

    task.Description = data.Description;
    task.UserFor = data.UserFor;
    task.Status = data.Status;

    $('#task_' + $('#edit-id').val()).remove();

    printTask(task);
    $.mobile.changePage("#main");
}

var saveNewTaskSuccess = function (data, textStatus, jqxhr) {
    console.log(data);
    tasks.push(data);
    printTask(data);

    $('#new-text').val('');
    $('#new-who').val(curUser.id);
    $('#new-who').selectmenu('refresh');
    $.mobile.changePage("#main");
}

var deleteTaskSuccess = function (data, textStatus, jqxhr) {
    var taskId = parseInt($('#edit-id').val());
    tasks = $.grep(tasks, function (t) { return t.Id !== taskId; });

    $('#task_' + taskId).remove();
    $.mobile.changePage("#main");
}

var updateUserLocationSuccess = function (data, textStatus, jqxhr) {
}

$(document).ready(initialize);

var sortFunction = function (a, b, sortingFunction) {
    if (sortingFunction(a) > sortingFunction(b)) return 1;
    if (sortingFunction(a) < sortingFunction(b)) return -1;
    if (sortingFunction(a) === sortingFunction(b)) return 0;
    return 0;
};

(function ($) {

    // Before handling a page change...
    $(document).bind("pagebeforechange", function (e, data) {
        // If the new page is not being loaded by URL, bail
        if (typeof data.toPage !== "string") {
            return;
        }

        // If the new page has a corresponding navbar link, activate its content div
        var url = $.mobile.path.parseUrl(data.toPage);
        var $a = $("div[data-role='navbar'] a[href='" + url.hash + "']");
        if ($a.length) {
            // Suppress normal page change handling since we're handling it here for this case
            e.preventDefault();
        }
            // If the new page has a navbar, activate the content div for its active item
        else {
            $a = $(url.hash + " div[data-role='navbar']").find("a.ui-btn-active");

            // Allow normal page change handling to continue in this case so the new page finishes rendering
        }

        // Show the content div to be activated and hide other content divs for this page
        var $content = $($a.attr("href"));
        $content.siblings().hide();
        $content.show();
    });

})(jQuery);