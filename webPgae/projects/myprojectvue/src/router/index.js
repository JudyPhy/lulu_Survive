import Vue from 'vue'
import Router from 'vue-router'
import Page1 from '@/components/page_1'
import Page2 from '@/components/page_2'
import PageRecommend from '@/components/page_recommend'

Vue.use(Router)

const originalPush = Router.prototype.push

Router.prototype.push = function push (location) {
  return originalPush.call(this, location).catch(err => err)
}

export default new Router({
  routes: [
    {
      path: '/page_recommend',
      component: PageRecommend
    },
    {
      path: '/page_1',
      component: Page1
    },
    {
      path: '/page_2',
      component: Page2
    },
    {
      path: '/',
      redirect: '/page_recommend'
    }
  ]
})
