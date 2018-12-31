// pages/test/test.js
const app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    height: 20,
    focus: false
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  
  //上传图片
  formReset(e){
    wx.chooseImage({
      count: 1,//可选择图片个数
      sizeType: ['original', 'compressed'],//指定原图还是压缩图
      sourceType: ['album', 'camera'],//相机还是本地
      success(res) {
        // tempFilePath可以作为img标签的src属性显示图片
         app.data.imagePath  = res.tempFilePaths
      }
    })

  },
   formSubmit(e) {
      console.log(app.data.imagePath)
      console.log('店名：', e.detail.value.input),
      console.log('描述：', e.detail.value.describe),
      console.log('详细信息：', e.detail.value.detail)
      var food = new Object;

      food.ItemsID = app.data.id2,
      food.ItemsDetail = e.detail.value.detail,
      food.ItemsDescribe = e.detail.value.describe,
      food.ItemsTitle = e.detail.value.input,
      food.ItemsImage = app.data.imagePath,
      console.log(e);
      console.log(food.ItemID);


       wx.request({
         url: 'http://localhost:54039/actionapi/Items/post',
         data: food,



         method: 'POST',
         header: { 'content-type': 'application/x-www-form-urlencoded'},
         success: function (res) {
           console.log('submit success');
         },
         fail: function (res) {
           console.log('submit fail');
         },
         complete: function (res) {
           console.log('submit complete');
         }

       })


  }
})