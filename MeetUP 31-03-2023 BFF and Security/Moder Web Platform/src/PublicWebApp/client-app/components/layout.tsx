import NavBar from './navbar'
import type { ReactNode } from 'react';

export default function Layout({ children } : {children: ReactNode} ) {
  return (
    <>
      <NavBar />
      <main>{children}</main>
    </>
  )
}
