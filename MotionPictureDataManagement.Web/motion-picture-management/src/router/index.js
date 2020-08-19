import { createRouter, createWebHashHistory } from 'vue-router'
import EditMotionPicture from '../components/EditMotionPicture'
import AddMotionPicture from '../components/AddMotionPicture'
import CopyMotionPicture from '../components/CopyMotionPicture'
import Home from '../components/Home';
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/motion-picture',
    name: 'Add New Motion Picture',
    component: AddMotionPicture
  },
  {
    path: '/motion-picture/:id',
    name: 'Edit Motion Picture',
    component: EditMotionPicture
  },
  {
    path: '/motion-picture/copy/:id',
    name: 'Copy Motion Picture',
    component: CopyMotionPicture
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
