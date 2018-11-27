import * as React from 'react'
import { connect } from 'react-redux'

class SimpleHome extends React.Component {
	componentDidMount() {
		window.document.title = 'Homepage | Web UI'
	}

	render() {
		return (
			<div>
				<h1>Web UI Homepage</h1>
			</div>
		)
	}
}

const Home = connect()(SimpleHome)

export { Home }
