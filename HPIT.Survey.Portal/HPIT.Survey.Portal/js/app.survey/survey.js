//window.onload = function () {
//    //alert("页面加载中。。。");
//};
var _ = {
    form: {},
    options: {
        "url": "/Survey/StartNewSurvey",
        "data": { "id": 11 },
        "type": "get",
        "datatype": "json"
    },
    render: function (survey) {
        $("#basic").tmpl(survey).appendTo('#div_basic');
        $("#enterprise").tmpl(survey).appendTo('#div_enterprise');
        $("#salary").tmpl(survey).appendTo('#div_salary');
        $("#project").tmpl(survey).appendTo('#div_project');
        $("#position").tmpl(survey).appendTo('#div_position');
        $("#job").tmpl(survey).appendTo('#div_job');
        //绘制各个步骤
        $("#wizard").bwizard().show();
    },
    getQueryString: function (name) {
        var reg = new RegExp('(?:(?:&|\\?)' + name + '=([^&]*))|(?:/' + name + '/([^/]*))', 'i');
        var r = window.location.href.match(reg);
        if (r != null)
            return decodeURI(r[1] || r[2]);
        return null;
    },
    ajaxData: function (options, callback) {
        $.ajax({
            url: options.url,
            data: options.data,
            type: options.type,
            dataType: options.dataType,
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (survey) {
                callback(survey);
            },
            complete: function () {
                $("#loading").hide();
                $('.datetime').datepicker({
                    autoclose: true,
                    format: 'yyyy-dd-mm'
                });
            }
        })
    }
};
$(document).ready(function () {
    var id = _.getQueryString("id");
    if (id != null && id != '') {
        _.options.url = "/Survey/GetSurveyByID";
        _.options.data = { "id": id };
    }
    _.ajaxData(_.options, function (result) {
        _.render(result);
        _.form = result.Form;
    })

    //检测界面上input标签的变化，如果有变化 则更改其中的值
    $("#wizard").on("change", "input", function () {
        var name = $(this).attr("name");
        if (name.split('.').length == 1) {
            _.form[name] = $(this).val();
        } else {
            var ss = name.split('.');
            _.form[ss[0]][ss[1]] = $(this).val();
        }
    })

    $("#wizard").on("change", "select", function () {
        var name = $(this).attr("name");
        if (name.split('.').length == 1) {
            _.form[name] = $(this).val();
        } else {
            var ss = name.split('.');
            _.form[ss[0]][ss[1]] = $(this).val();
        }
    
    })

    $("#div_job").on("click", "#save", function () {
        _.options.url = "/Survey/Save";
        _.options.type = 'post';
        _.options.data = _.form;
        _.ajaxData(_.options, function (result) {
            alert("成功!");
        });
    })

    $("#wizard").on("click", "#addProject", function () {
        var name = $("#projectName").val();
        var type = $("#projectType").val();
        var desc = $("#projectdesc").val();
        alert("添加项目" + name + type + desc);
        _.form.Project.push({ "ProjectName": name, "ProjectType": type, "ProjectDesc": desc });
    })

    $("#wizard").on("click", "#addPosition", function () {
        alert("添加岗位");
    })

    $("#wizard").on("click", "#addjob", function () {
        alert("添加职位");
    })

})