import useSWR from 'swr'

const useValues = () => {

  const headers = {
    'x-csrf': '1'
  }

  const fetcher = (url : string, headers : any) => fetch(url, { headers }).then(res => res.json())

  const { data, error } = useSWR(['/api/values', headers], fetcher)
  return {
    values: data,
    isLoading: !error && !data,
    isError: error
  }
}

export default function Values() {
  const { values, isLoading, isError  } = useValues()

  if (isLoading) return <div>Loading</div>
  if (isError) return <div>Error</div>
  return values.map((v: string) => (<p key={v}>{v}</p>))
}
