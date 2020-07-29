import Vue from 'vue'
import Router from 'vue-router'
import PageTV from '@/components/page_tv'
import Page2 from '@/components/page_2'

Vue.use(Router)

const originalPush = Router.prototype.push

Router.prototype.push = function push (location) {
  return originalPush.call(this, location).catch(err => err)
}

export default new Router({
  routes: [
    {
      path: '/page_tv',
      component: PageTV
    },
    {
      path: '/page_2',
      component: Page2
    },
    {
      path: '/',
      redirect: '/page_base'
    }
  ]
})
