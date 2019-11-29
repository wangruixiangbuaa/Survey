//window.onload = function () {
//    //alert("页面加载中。。。");
//};
var _ = {
    MasterData: {},
    options: {
        "url": "/Survey/StartNewSurvey",
        "data": { "id": 11 },
        "type": "get",
        "datatype": "json"
    },
    uuid: function() {
        var s = [];
        var hexDigits = "0123456789abcdef";
        for (var i = 0; i < 36; i++) {
            s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
        }
        s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
        s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
        s[8] = s[13] = s[18] = s[23] = "-";

        var uuid = s.join("");
        return uuid;
    },
    render: function (survey) {
        $("#basic").tmpl(survey).appendTo('#div_basic');
        $("#enterprise").tmpl(survey).appendTo('#div_enterprise');
        $("#salary").tmpl(survey).appendTo('#div_salary');
        $("#projectC").tmpl(survey).appendTo('#div_project');
        $("#positionC").tmpl(survey).appendTo('#div_position');
        $("#jobC").tmpl(survey).appendTo('#div_job');

        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
        $("#position").tmpl(_.MasterData).appendTo('#positionTb');
        $("#job").tmpl(_.MasterData).appendTo('#jobTb');
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
                    format: 'yyyy-mm-dd'
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
    } else {
        var type = $("#main_div").attr("stype");
        var stuNo = _.getQueryString("stuNo");
        if (type == 0) {
            _.options.url = "/Survey/StartUserNewSurvey";
        } else {
            _.options.url = "/Survey/StartStudentNewSurvey?stuNo=" + stuNo;
        }
    }
    _.ajaxData(_.options, function (result) {
        _.MasterData = result;
        _.render(_.MasterData);
    })

    //检测界面上input标签的变化，如果有变化 则更改其中的值
    $("#wizard").on("change", "input", function () {
        var name = $(this).attr("name");
        if (name == null || name == undefined) {
            return false;
        }
        //学生姓名发生变化的时候
        if (name == "StuName") {
            var nvalue = $(this).val();
            _.options.url = "/Survey/QueryStudentInfo?name=" + nvalue +"&className="+'';
            _.options.data = {};
            _.ajaxData(_.options, function (result) {
                //console.log(result);
                if (result.State == 'OK') {
                    _.MasterData.Form.ProjectName = result.Data.pName;
                    _.MasterData.Form.School = result.Data.GraduateSchool;
                    _.MasterData.Form.Batch = result.Data.bName;
                    _.MasterData.Form.Phone = result.Data.Mobile;
                    _.MasterData.Form.Year = result.Data.bYear;
                    _.MasterData.Form.Direction = result.Data.mName;
                    $('#step1').html('');
                    $("#basic").tmpl(_.MasterData).appendTo('#step1');
                };
                
            })
        }
        if (name.split('.').length == 1) {
            _.MasterData.Form[name] = $(this).val();
        }
        else {
            var ss = name.split('.');
            _.MasterData.Form[ss[0]][ss[1]] = $(this).val();
        }
    })

    $("#wizard").on("change", "textarea", function () {
        var name = $(this).attr("name");
        if (name == null || name == undefined) {
            return false;
        }
        if (name.split('.').length == 1) {
            _.MasterData.Form[name] = $(this).val();
        } else {
            var ss = name.split('.');
            _.MasterData.Form[ss[0]][ss[1]] = $(this).val();
        }

    })

    $("#wizard").on("change", "select", function () {
        var name = $(this).attr("name");
        if (name == null || name == undefined) {
            return false;
        }
        if (name.split('.').length == 1) {
            _.MasterData.Form[name] = $(this).val();
        } else {
            var ss = name.split('.');
            _.MasterData.Form[ss[0]][ss[1]] = $(this).val();
        }

    })

    $("#div_job").on("click", "#save", function () {
        if (_.MasterData.Form.SurveyID == 0) {
            _.options.url = "/Survey/Save";
        } else {
            _.options.url = "/Survey/Update";
        }
        _.options.type = 'post';
        _.options.data = _.MasterData.Form;
        _.ajaxData(_.options, function (result) {
            //alert("反馈成功!");
            //swal("", "欢迎您的反馈！", "success");
            swal({
                title: "欢迎您的反馈！",
                showConfirmButton: false,
                type:"success",
                showCancelButton: false,
                timer: 2000
            })
            setTimeout(function () {
                window.close();
                //5秒后实现的方法写在这个方法里面

            }, 2 * 1000)
        });
    })

    $("#wizard").on("click", "#addProject", function () {
        var name = $("#projectName").val();
        var type = $("#projectType").val();
        var desc = $("#projectdesc").val();
        //alert("添加项目" + name + type + desc);
        _.MasterData.Form.Project.push({ "ProjectName": name, "ProjectType": type, "ProjectDesc": desc });
        $("#projectTb tr:gt(0)").remove();
        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
    })

    $("#projectTb").on("click", "a", function () {
        var name = $(this).attr("pname");
        var newPro = [];
        for (var i = 0; i < _.MasterData.Form.Project.length; i++) {
            if (_.MasterData.Form.Project[i].ProjectName != name) {
                newPro.push(_.MasterData.Form.Project[i]);
            }
        }
        _.MasterData.Form.Project = newPro;
        $("#projectTb tr:gt(0)").remove();
        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
    })

    $("#wizard").on("click", "#addPosition", function () {
        var name = $("#positionName").val();
        var type = $("#positionDirection").val();
        var source = $("#positionSource").val();
        var desc = $("#positionDesc").val();
        var years = $("#positionYears").val();
        var uuid = _.uuid();
        _.MasterData.Form.Position.push({ "PositionID": uuid, "PositionName": name, "PositionType": type, "Source": source, "PositionDesc": desc, "Years": years });
        $("#positionTb tr:gt(0)").remove();
        var select = $("#positionList");
        $("#positionList option").remove();
        for (var i = 0; i < _.MasterData.Form.Position.length; i++) {
            var text = _.MasterData.Form.Position[i].PositionName;
            var value = _.MasterData.Form.Position[i].PositionID
            $("#positionList").append("<option value='" + value + "'>" + text + "</option>");
        }
        $("#position").tmpl(_.MasterData).appendTo('#positionTb');
    })


    $("#positionTb").on("click", "a", function () {
        var name = $(this).attr("pname");
        var newPro = [];
        for (var i = 0; i < _.MasterData.Form.Position.length; i++) {
            if (_.MasterData.Form.Position[i].PositionName != name) {
                newPro.push(_.MasterData.Form.Position[i]);
            }
        }
        _.MasterData.Form.Position = newPro;
        $("#projectTb tr:gt(0)").remove();
        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
    })

    $("#wizard").on("click", "#addjob", function () {
        var pid = $("#positionList").val();
        var text = $("#positionList").find("option:selected").text();
        var count = $("#jobCount").val();
        var begin = $("#jobBeginTime").val();
        var end = $("#jobEndTime").val();
        var avgMoney = $("#avgMoney").val();
        var desc = "";
        var type = "";
        var match = {};
        for (var i = 0; i < _.MasterData.Form.Position.length; i++) {
            if (_.MasterData.Form.Position[i].PositionName == name) {
                desc = _.MasterData.Form.Position[i].PositionDesc;
                type = _.MasterData.Form.Position[i].PositionType;
            }
        }
        _.MasterData.Form.ActiveJobs.push({ "PositionID": pid, "JobName": text, "JobType": type, "JobDesc": desc, "StartTime": begin, "EndTime": end, "Count": count, "AvageMoney": avgMoney });
        $("#jobTb tr:gt(0)").remove();
        $("#job").tmpl(_.MasterData).appendTo('#jobTb');
    })

    $("#jobTb").on("click", "a", function () {
        var name = $(this).attr("pname");
        var newPro = [];
        for (var i = 0; i < _.MasterData.Form.ActiveJobs.length; i++) {
            if (_.MasterData.Form.Position[i].JobName != name) {
                newPro.push(_.MasterData.Form.ActiveJobs[i]);
            }
        }
        _.MasterData.Form.ActiveJobs = newPro;
        $("#projectTb tr:gt(0)").remove();
        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
    })

})