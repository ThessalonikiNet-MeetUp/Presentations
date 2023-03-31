import Link from 'next/link'
import UserInfo from './userInfo';

export default function Navbar() {
  return (
    <nav className="relative w-full flex flex-wrap items-center justify-between py-3 bg-gray-100 text-gray-500 hover:text-gray-700 focus:text-gray-700 shadow-lg">
      <div className="container-fluid w-full flex flex-wrap items-center justify-between px-6">
          <a className="text-xl text-black" href="#">Navbar</a>
          <ul className="navbar-nav flex flex-row pl-0 list-style-none mr-auto ">
            <li className="nav-item px-2">
              <Link className="nav-link" href="/">Home</Link>
            </li>
            <li className="nav-item px-2">
              <Link className="nav-link" href="/values">Values</Link>
            </li>
          </ul>
          <div className="flex items-center relative">
            <UserInfo />
          </div>
      </div>
    </nav>
  )
}
