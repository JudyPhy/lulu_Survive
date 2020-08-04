import Vue from 'vue'
import Router from 'vue-router'
import PageTV from '@/components/page_tv'
import PageEpisodes from '@/components/page_2'
import PageVedioPlay from '@/components/page_vedio_play'

Vue.use(Router)

const originalPush = Router.prototype.push

Router.prototype.push = function push (location) {
  return originalPush.call(this, location).catch(err => err)
}

export default new Router({
  routes: [
    {
      path: '/page_tv',
      component: PageTV,
      meta: {
        showNav: true
      }
    },
    {
      path: '/page_episodes',
      component: PageEpisodes,
      meta: {
        showNav: true
      }
    },
    {
      path: '/page_vedio_play',
      component: PageVedioPlay,
      meta: {
        showNav: false
      }
    },
    {
      path: '/',
      redirect: '/page_tv',
      meta: {
        showNav: true
      }
    }
  ]
})
