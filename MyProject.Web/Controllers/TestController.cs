﻿using MyProject.Application;
using MyProject.Application.Dtos;
using MyProject.Framework.Mvc;
using MyProject.Framework.Paging;
using MyProject.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Web.Controllers
{
    /// <summary>
    /// 示例代码
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseApiController
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// 事务测试
        /// </summary>
        /// <returns>标准Json返回格式,返回值为Data</returns>
        [HttpGet("TransactionTest")]
        public StandardJsonResult TransactionTest()
        {
            return StandardAction(()=> { });
        }

        /// <summary>
        /// 添加测试
        /// </summary>
        /// <param name="dto">StudentDto</param>
        /// <returns>StudentDto</returns>
        [HttpPost("PostStudent")]
        public StandardJsonResult<StudentDto> PostStudent(StudentDto dto)
        {
            return StandardAction(() => _testService.AddStudent(dto));
        }

        /// <summary>
        /// 分页测试
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStudentByPage")]
        public StandardJsonResult<PageResult<StudentDto>> GetStudentByPage(PageParam pageParam)
        {
            return StandardAction(() => _testService.GetStudents(pageParam));
        }
    }
}