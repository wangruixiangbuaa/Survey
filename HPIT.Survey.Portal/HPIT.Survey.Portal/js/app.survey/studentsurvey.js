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
    checkZch15: function (num) {
        if (num.length != 15) {
            return "请输入15存数字";
        }
        var reg = /^[0-9]*$/;
        if (!reg.test(num)) {
            return "请输入纯数字";
        }
        var a = num.split("");
        var p = [], s = [];
        var temp = 0;
        p[0] = 10;
        s[0] = (p[0] % 11) * 1 + a[0] * 1;
        for (var j = 1; j < a.length; j++) {
            temp = s[j - 1] % 10;
        }
        if (temp == 0) {
            temp = 10;
        }
        p[j] = temp * 2;
        s[j] = (p[j] % 11) * 1 + a[j] * 1;
        if (11 - p[14] >= 0) {
            temp = 11 - p[14];
        } else {
            temp = 22 - p[14];
        }

        if (temp == 10) {
            temp = 0;
        }
        if (temp == a[14]) {
            return true;
        }
        return "校验失败";
    },
    uuid: function () {
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
        //处理可以输入的下拉框的控件 处理
        try {
            $("#companyType").editableSelect({
                bg_iframe: true,
                case_sensitive: false,
                items_then_scroll: 10,
                isFilter: false
            });
            $("#companyType_sele").val(_.MasterData.Form.Company.CompanyType);
            $("#positionSource").editableSelect({
                bg_iframe: true,
                case_sensitive: false,
                items_then_scroll: 10,
                isFilter: false
            });
            $("#projectType").editableSelect({
                bg_iframe: true,
                case_sensitive: false,
                items_then_scroll: 10,
                isFilter: false
            });
        } catch (e) {
            console.log("下拉框,初始化报错了。");
        }
        //初始化分步的界面显示
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
                //初始化日期控件
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
        //判断是不是查看功能
        if (result.CurrentRole != null) {
            if (result.CurrentRole.FullName != "学生" && result.CurrentRole.FullName != '人事经理') {
                $('#save').hide();
                $('#addProject').hide();
                $('#addPosition').hide();
                $('#addjob').hide();
            }
        };
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
            _.options.url = "/Survey/QueryStudentInfo?name=" + encodeURI(nvalue) + "&className=" + encodeURI('');
            _.options.data = {};
            _.ajaxData(_.options, function (result) {
                if (result.State == 'OK') {
                    _.MasterData.Form.ProjectName = result.Data.pName;
                    _.MasterData.Form.School = result.Data.GraduateSchool;
                    _.MasterData.Form.Batch = result.Data.bName;
                    _.MasterData.Form.Phone = result.Data.Mobile;
                    _.MasterData.Form.Year = result.Data.bYear;
                    _.MasterData.Form.Direction = result.Data.mName;
                    //重新加载数据，渲染界面
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
            if (name == "Company.CompanyType") {
                var value = $(this).val();
                var parentid = 0;
                for (var i = 0; i < _.MasterData.ExtraDatas.Industrys.length; i++) {
                    if (_.MasterData.ExtraDatas.Industrys[i].Value == value) {
                        parentid = _.MasterData.ExtraDatas.Industrys[i].ID;
                    }
                }
                _.MasterData.ExtraDatas.SecondIndustrys = [];
                for (var i = 0; i < _.MasterData.ExtraDatas.DetailIndustrys.length; i++) {
                    if (_.MasterData.ExtraDatas.DetailIndustrys[i].parentID == parentid) {
                        var text = _.MasterData.ExtraDatas.DetailIndustrys[i].Text;
                        var value = _.MasterData.ExtraDatas.DetailIndustrys[i].Value;
                        _.MasterData.ExtraDatas.SecondIndustrys.push({ "Text": text, "Value": value });
                    }
                }
                if (_.MasterData.ExtraDatas.SecondIndustrys.length > 0) {
                    _.MasterData.Form['Company']['CompanyDetailType'] = _.MasterData.ExtraDatas.SecondIndustrys[0].Value;
                }
                _.MasterData.Form[ss[0]][ss[1]] = $(this).val();
                $('#step1').html('');
                $("#enterprise").tmpl(_.MasterData).appendTo('#step1');
            }
            //var nameval = $("input[name=" + name + "']").val();
            _.MasterData.Form[ss[0]][ss[1]] = $(this).val();
        }

    })

    $("#div_job").on("click", "#save", function () {
        //获取下拉框选择的公司类型的值。
        //_.MasterData.Form.Company.CompanyType = $("input[name='Company.CompanyType_sele']").val();
        if (_.MasterData.Form.SurveyID == 0) {
            _.options.url = "/Survey/Save";
        } else {
            _.options.url = "/Survey/Update";
        }
        _.options.type = 'post';
        _.options.data = _.MasterData.Form;
        var form = _.MasterData.Form;
        if (form.StuName == '' || form.StuName == null) {
            swal("", "学生姓名不能为空！", "info");
            return;
        }
        if (form.School == '' || form.School == null) {
            swal("", "学校不能为空！", "info");
            return;
        }
        if (form.ProjectName == '' || form.ProjectName == null) {
            swal("", "项目部不能为空！", "info");
            return;
        }
        //未就业学生都需要校验
        if (form.Company.CompanyName == '' || form.Company.CompanyName == null) {
            swal("", "公司名不能为空！", "info");
            return;
        }
        if (form.Company.CompanyDetailType == '' || form.Company.CompanyDetailType == null) {
            swal("", "公司行业不能为空！", "info");
            return;
        }
        if (form.CompanyNo == '' || form.CompanyNo == null) {
            swal("", "公司工商号不能为空！", "info");
            return;
        }
        var regExp = /^[0-9]*$/;
        if (form.CompanyNo.length != 15 || !regExp.test(form.CompanyNo)) {
            swal("", "公司工商号不符合要求(15位数字)", "info");
            return;
        }
        if (form.Position.length == 0) {
            swal("", "至少填写一个岗位招聘信息！", "info");
            return;
        }
        if (form.ActiveJobs.length == 0) {
            swal("", "至少填写一个公司正在招聘的职位！", "info");
            return;
        }
        _.ajaxData(_.options, function (result) {
            //alert("反馈成功!");
            //swal("", "欢迎您的反馈！", "success");
            swal({
                title: "欢迎您的反馈！",
                showConfirmButton: false,
                type: "success",
                showCancelButton: false,
                timer: 2000
            })
            setTimeout(function () {
                window.close();
                //5秒后实现的方法写在这个方法里面
            }, 2 * 1000)
        });
    })

    //添加项目
    $("#wizard").on("click", "#addProject", function () {
        var name = $("#projectName").val();
        var type = $("#projectType_sele").val(); //$("#projectType").val();
        var desc = $("#projectdesc").val();
        //alert("添加项目" + name + type + desc);
        _.MasterData.Form.Project.push({ "ProjectName": name, "ProjectType": type, "ProjectDesc": desc });
        $("#projectTb tr:gt(0)").remove();
        $("#project").tmpl(_.MasterData).appendTo('#projectTb');
    })

    //加载下拉列表
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
        var source = $("#positionSource_sele").val(); //$("#positionSource").val();
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

    //删除操作
    $("#positionTb").on("click", "a", function () {
        var name = $(this).attr("pname");
        var newPro = [];
        for (var i = 0; i < _.MasterData.Form.Position.length; i++) {
            if (_.MasterData.Form.Position[i].PositionName != name) {
                newPro.push(_.MasterData.Form.Position[i]);
            }
        }
        _.MasterData.Form.Position = newPro;
        $("#positionTb tr:gt(0)").remove();
        $("#position").tmpl(_.MasterData).appendTo('#projectTb');
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
        $("#jobTb tr:gt(0)").remove();
        $("#job").tmpl(_.MasterData).appendTo('#jobTb');
    })

})