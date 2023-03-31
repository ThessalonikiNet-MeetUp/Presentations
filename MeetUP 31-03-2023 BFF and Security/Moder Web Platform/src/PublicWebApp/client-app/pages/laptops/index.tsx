import Link from 'next/link'

export default function Laptops() {
  return (
    <div>
      <div>
      <ul>
        <li><Link href="laptops/hp">HP</Link></li>
        <li><Link href="laptops/dell">Dell</Link></li>
      </ul>
      </div>
      <h4>This is the laptops category page</h4>
    </div>
  )
}
