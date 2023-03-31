import Link from 'next/link'
import { useState } from 'react';
import useSWR from 'swr'

const useUser = () => {
  const headers = {
    'x-csrf': '1'
  }

  const fetcher = async (url : string, headers : any ) => {
    const response = await fetch(url, { headers })

    if (!response.ok) {
      const error = new Error()
      error.message = 'Unauthorized'
      throw error
    }

    return response.json()
  }

  const { data, error } = useSWR(['/bff/user', headers], fetcher)

  return {
    user: data,
    isLoading: !error && !data,
    isError: error
  }
}

export default function UserInfo() {
  const { user, isLoading, isError  } = useUser()
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [logoutUrl, setLogoutUrl] = useState('')


  if(isError) {
    console.log(isError)
  }
  if (!isLoading && !isError && user && !isAuthenticated) {
    setIsAuthenticated(true)
    setLogoutUrl(user.find((u : { type: string; }) => u.type === 'bff:logout_url').value)
  }

  if(!isAuthenticated) {
    return (
      <Link className="nav-link inline-block px-6 py-2.5 bg-blue-600 text-white font-medium text-xs leading-tight uppercase rounded shadow-md hover:bg-blue-700 hover:shadow-lg focus:bg-blue-700 focus:shadow-lg focus:outline-none focus:ring-0 active:bg-blue-800 active:shadow-lg transition duration-150 ease-in-out" href="/bff/login">Login</Link>
      )
    }
    else {
      const userName = user.find((u : { type: string; }) => u.type === 'name').value
      return (
        <div>
          <span>
            Welcome {userName}
          </span>
          <Link
            className="nav-link inline-block px-6 py-2.5 bg-blue-600 text-white font-medium text-xs leading-tight uppercase rounded shadow-md hover:bg-blue-700 hover:shadow-lg focus:bg-blue-700 focus:shadow-lg focus:outline-none focus:ring-0 active:bg-blue-800 active:shadow-lg transition duration-150 ease-in-out"
            href={logoutUrl}>
              Logout
          </Link>
        </div>
      )
    }
}
