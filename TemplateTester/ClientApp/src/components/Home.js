import React from 'react'
import { connect } from 'react-redux'
import EntityViewer from './EntityViewer'

class Home extends React.Component {
	componentDidMount() {
		window.document.title = 'Web UI | Homepage'
	}

	render() {
		return (
			<div>
				<h1>Web UI Homepage</h1>
				<EntityViewer />
			</div>
		)
	}
}

export default connect()(Home)
